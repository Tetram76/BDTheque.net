namespace BDTheque.Data.Validators;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using FluentValidation;

public class GenreValidator(IGenreRepository genreRepository) : EntityValidator<Genre>
{
    protected override IEditableEntityRepository<Genre> EntityRepository => genreRepository;

    protected override void GetDefaultRuleSet()
    {
        base.GetDefaultRuleSet();

        RuleFor(genre => genre.Nom)
            .MustAsync(async (genre, _, cancellationToken) => await genreRepository.IsNameAllowed(genre, cancellationToken))
            .WithMessage("{PropertyName} \"{PropertyValue}\" is already used")
            .WithName("Genre name")
            .When(genre => MustValidate(genre, e => e.Nom));
    }
}
