namespace BDTheque.Tests;

using Xunit.Abstractions;

public class BaseTest(ITestOutputHelper testOutputHelper) : IDisposable
{
    private readonly List<Func<Task>> _finalizers = [];

    public ITestOutputHelper TestOutputHelper { get; } = testOutputHelper;

    protected void RegisterFinalizer(Action action) =>
        _finalizers.Add(() => Task.Run(action));

    protected void RegisterFinalizer(Func<Task> action) =>
        _finalizers.Add(action);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;

        _finalizers.Reverse();
        foreach (Func<Task> action in _finalizers)
            action().Wait();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
