namespace BDTheque.GraphQL.Mutations.Inputs;

using BDTheque.Model.Entities.Abstract;

public abstract class SimpleIdInputType<T> : VersioningInputType<T>
    where T : SimpleIdEntity;
