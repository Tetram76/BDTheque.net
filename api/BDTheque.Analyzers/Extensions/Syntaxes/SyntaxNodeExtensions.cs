// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis;

using System.Diagnostics.CodeAnalysis;

using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

public static class SyntaxNodeExtensions
{
    public static NameSyntax ContainingNamespace(this SyntaxNode syntaxNode) =>
        syntaxNode.Ancestors()
            .Where(node => node is NamespaceDeclarationSyntax or FileScopedNamespaceDeclarationSyntax)
            .Select(
                node => node switch
                {
                    NamespaceDeclarationSyntax namespaceDeclarationSyntax => namespaceDeclarationSyntax.Name,
                    FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax => fileScopedNamespaceDeclarationSyntax.Name,
                    _ => throw new ArgumentOutOfRangeException(nameof(node))
                }
            )
            .First();

    public static SemanticModel SemanticModel(this SyntaxNode syntaxNode, GeneratorSyntaxContext context) =>
        syntaxNode.SyntaxTree == context.SemanticModel.SyntaxTree ? context.SemanticModel : context.SemanticModel.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

    public static SemanticModel SemanticModel(this SyntaxNode syntaxNode, SyntaxNodeAnalysisContext context) =>
        syntaxNode.SyntaxTree == context.SemanticModel.SyntaxTree ? context.SemanticModel : context.SemanticModel.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

    public static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, string attributeClassFullname, GeneratorSyntaxContext context) =>
        syntaxNode.IsAnnotatedWithAttribute(attributeClassFullname, syntaxNode.SemanticModel(context), out _);

    public static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, string attributeClassFullname, SyntaxNodeAnalysisContext context) =>
        syntaxNode.IsAnnotatedWithAttribute(attributeClassFullname, syntaxNode.SemanticModel(context), out _);

    public static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, string attributeClassFullname, SemanticModel semanticModel, [NotNullWhen(true)] out AttributeSyntax? attributeSyntaxNode)
    {
        attributeSyntaxNode = null;
        if (semanticModel.GetDeclaredSymbol(syntaxNode) is not { } symbol)
            return false;

        if (!symbol.IsAnnotatedWithAttribute(attributeClassFullname, out AttributeData? attributeData))
            return false;

        SyntaxReference? attributeSyntaxReference = attributeData.ApplicationSyntaxReference;
        if (attributeSyntaxReference != null)
            attributeSyntaxNode = attributeSyntaxReference.GetSyntax() as AttributeSyntax;

        return attributeSyntaxNode != null;
    }
}
