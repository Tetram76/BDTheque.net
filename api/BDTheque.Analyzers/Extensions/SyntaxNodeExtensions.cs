namespace BDTheque.Analyzers.Extensions;

using Microsoft.CodeAnalysis;

public static class SyntaxNodeExtensions
{
    public static SemanticModel SemanticModel(this SyntaxNode syntaxNode, GeneratorSyntaxContext context) =>
        syntaxNode.SyntaxTree == context.SemanticModel.SyntaxTree ? context.SemanticModel : context.SemanticModel.Compilation.GetSemanticModel(syntaxNode.SyntaxTree);

    public static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, string attributeClassFullname, GeneratorSyntaxContext context) =>
        syntaxNode.IsAnnotatedWithAttribute(attributeClassFullname, context, out _);

    private static bool IsAnnotatedWithAttribute(this SyntaxNode syntaxNode, string attributeClassFullname, GeneratorSyntaxContext context, out AttributeData? attribute)
    {
        var semanticModel = syntaxNode.SemanticModel(context);
        attribute = null;
        if (semanticModel.GetDeclaredSymbol(syntaxNode) is not { } nodeSymbol) return false;
        if (semanticModel.Compilation.GetTypeByMetadataName(attributeClassFullname) is not { } attributeSymbol) return false;

        attribute = nodeSymbol
            .GetAttributes()
            .FirstOrDefault(attr => attr.AttributeClass != null && attr.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));

        return attribute != default;
    }
}
