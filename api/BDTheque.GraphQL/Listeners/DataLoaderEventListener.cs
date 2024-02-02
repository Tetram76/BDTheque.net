namespace BDTheque.GraphQL.Listeners;

public class DataLoaderEventListener : DataLoaderDiagnosticEventListener
{
    public override IDisposable ExecuteBatch<TKey>(IDataLoader dataLoader, IReadOnlyList<TKey> keys) =>
        EmptyScope;
}
