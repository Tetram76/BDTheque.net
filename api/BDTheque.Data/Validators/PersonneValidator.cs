namespace BDTheque.Data.Validators;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using FluentValidation;

public class PersonneValidator(IPersonneRepository personneRepository) : EntityValidator<Personne>
{
    protected override IEditableEntityRepository<Personne> EntityRepository => personneRepository;

    protected override void GetDefaultRuleSet()
    {
        base.GetDefaultRuleSet();

        RuleFor(personne => personne.Nom)
            .MustAsync(async (personne, _, cancellationToken) => await personneRepository.IsNameAllowed(personne, cancellationToken))
            .WithMessage("{PropertyName} \"{PropertyValue}\" is already used")
            .WithName("Personne name")
            .When(personne => MustValidate(personne, e => e.Nom));
    }
}
