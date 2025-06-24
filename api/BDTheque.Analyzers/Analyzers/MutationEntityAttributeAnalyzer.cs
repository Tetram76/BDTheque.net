namespace BDTheque.Analyzers.Analyzers;

using System.Collections.Immutable;

using BDTheque.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class MutationEntityAttributeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
    [
        WellKnownDiagnosticDescriptors.NotPartialDescriptor
    ];

    public override void Initialize(AnalysisContext context)
    {
        // Ne pas analyser les nœuds inactifs ou générés
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax) context.Node;
        if (!classDeclaration.IsAnnotatedWithMutationType(context))
            return;
        if (!classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
            context.ReportDiagnostic(Diagnostic.Create(WellKnownDiagnosticDescriptors.NotPartialDescriptor, classDeclaration.GetLocation(), classDeclaration.Identifier));
    }
}
