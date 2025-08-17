namespace BDTheque.GraphQL.Listeners;

using GreenDonut;

public class DataLoaderEventListener : DataLoaderDiagnosticEventListener
{
    public override IDisposable ExecuteBatch<TKey>(IDataLoader dataLoader, IReadOnlyList<TKey> keys) =>
        EmptyScope;
}
