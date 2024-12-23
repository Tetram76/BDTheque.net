// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.Hosting;

public static class HostEnvironmentEnvExtensions
{
    private const string ContinuousIntegration = "CI";

    public static bool IsContinuousIntegration(this IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsEnvironment(ContinuousIntegration);
}
