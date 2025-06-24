namespace BDTheque.Analyzers.CodeFixes;

using System.Collections.Immutable;

using BDTheque.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(PartialClassCodeFix))]
public class PartialClassCodeFix : CodeFixProvider
{
    public override ImmutableArray<string> FixableDiagnosticIds =>
    [
        WellKnownDiagnosticDescriptors.NotPartialDescriptor.Id
    ];

    public override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        // Trouver le nœud syntaxique à corriger
        if (await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false) is not { } root)
            return;

        var node = root.FindToken(diagnosticSpan.Start).Parent?.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();

        // Créer une action de correction
        context.RegisterCodeFix(
            CodeAction.Create(
                title: "Make class partial",
                createChangedDocument: cancellationToken => MakeClassPartialAsync(context.Document, node, cancellationToken),
                equivalenceKey: "Make class partial"
            ),
            diagnostic
        );
    }

    private static async Task<Document> MakeClassPartialAsync(Document document, ClassDeclarationSyntax? classDeclaration, CancellationToken cancellationToken)
    {
        if (classDeclaration is null || classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword))
            return document;

        var newClassDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword));

        if (await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false) is not { } root)
            return document;

        var newRoot = root.ReplaceNode(classDeclaration, newClassDeclaration);

        return document.WithSyntaxRoot(newRoot);
    }
}
