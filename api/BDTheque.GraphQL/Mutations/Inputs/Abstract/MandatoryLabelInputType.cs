namespace BDTheque.GraphQL.Mutations.Inputs;

using BDTheque.Model.Entities.Abstract;

public abstract class MandatoryLabelInputType<T> : AssociableInputType<T>
    where T : MandatoryLabelEntity;