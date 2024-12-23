namespace BDTheque.Data.Validators;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using FluentValidation;

public class EditeurValidator(IEditeurRepository editeurRepository) : EntityValidator<Editeur>
{
    protected override IEditableEntityRepository<Editeur> EntityRepository => editeurRepository;

    protected override void GetDefaultRuleSet()
    {
        base.GetDefaultRuleSet();

        RuleFor(editeur => editeur.Nom)
            .MustAsync(async (editeur, _, cancellationToken) => await editeurRepository.IsNameAllowed(editeur, cancellationToken))
            .WithMessage("{PropertyName} \"{PropertyValue}\" is already used")
            .WithName("Editeur name")
            .When(editeur => MustValidate(editeur, e => e.Nom));
    }
}
