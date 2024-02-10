namespace BDTheque.GraphQL.Mutations.Inputs;

using BDTheque.Model.Entities.Abstract;

public abstract class UniqueIdInputType<T> : VersioningInputType<T>
    where T : UniqueIdEntity;
