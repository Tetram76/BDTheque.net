namespace BDTheque.Data.Validators;

using BDTheque.Data.Repositories.Interfaces;
using BDTheque.Model.Entities;

using FluentValidation;

public class CollectionValidator(ICollectionRepository collectionRepository) : EntityValidator<Collection>
{
    protected override IEditableEntityRepository<Collection> EntityRepository => collectionRepository;

    protected override void GetDefaultRuleSet()
    {
        base.GetDefaultRuleSet();

        RuleFor(collection => collection.Nom)
            .MustAsync(async (collection, _, cancellationToken) => await collectionRepository.IsNameAllowed(collection, cancellationToken))
            .WithMessage("{PropertyName} \"{PropertyValue}\" is already used")
            .WithName("Collection name")
            .When(collection => MustValidate(collection, e => e.Nom));
    }
}
