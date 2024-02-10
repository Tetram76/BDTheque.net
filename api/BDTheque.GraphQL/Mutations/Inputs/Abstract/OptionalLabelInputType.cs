namespace BDTheque.GraphQL.Mutations.Inputs;

using BDTheque.Model.Entities.Abstract;

public abstract class OptionalLabelInputType<T> : AssociableInputType<T>
    where T : OptionalLabelEntity;
