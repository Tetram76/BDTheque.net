namespace BDTheque.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;

public static class WellKnownDiagnosticDescriptors
{
    private static readonly string DiagnosticCategory = typeof(DiagnosticDescriptor).Assembly.GetName().Version.ToString();

    public static readonly DiagnosticDescriptor DebugInfoDescriptor = new(
        id: "BDDBG001",
        title: "Debug step",
        messageFormat: "Debug info: {0}",
        category: "Debug",
        DiagnosticSeverity.Info,
        isEnabledByDefault: true
    );

    public static readonly DiagnosticDescriptor NotPartialDescriptor = new(
        id: "BDERR001",
        title: "Partial class",
        messageFormat: "{0} class must be partial",
        category: DiagnosticCategory,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}
