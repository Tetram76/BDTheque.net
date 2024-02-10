namespace BDTheque.GraphQL.Mutations.Inputs;

using BDTheque.Model.Entities.Abstract;

public abstract class AssociableInputType<T> : UniqueIdInputType<T>
    where T : AssociableEntity;
