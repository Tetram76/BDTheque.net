namespace BDTheque.Tests;

public class BaseTest : IDisposable
{
    private readonly List<Func<Task>> _finalizers = [];

    protected void RegisterFinalizer(Action action) =>
        _finalizers.Add(() => Task.Run(action));

    protected void RegisterFinalizer(Func<Task> action) =>
        _finalizers.Add(action);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _finalizers.ForEach(action => action());
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
