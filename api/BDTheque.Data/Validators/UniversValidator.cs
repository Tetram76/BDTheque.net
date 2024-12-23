namespace BDTheque.Data.Validators;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using FluentValidation;

public class UniversValidator(IUniversRepository universRepository) : EntityValidator<Univers>
{
    protected override IEditableEntityRepository<Univers> EntityRepository => universRepository;

    protected override void GetDefaultRuleSet()
    {
        base.GetDefaultRuleSet();

        RuleFor(univers => univers.Nom)
            .MustAsync(async (univers, _, cancellationToken) => await universRepository.IsNameAllowed(univers, cancellationToken))
            .WithMessage("{PropertyName} \"{PropertyValue}\" is already used")
            .WithName("Univers name")
            .When(univers => MustValidate(univers, e => e.Nom));
    }
}
