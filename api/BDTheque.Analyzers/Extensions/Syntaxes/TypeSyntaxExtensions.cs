// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis.CSharp.Syntax;

public static class TypeSyntaxExtensions
{
    public static bool IsEntityType(this TypeSyntax typeSyntax, GeneratorSyntaxContext context)
    {
        SemanticModel semanticModel = typeSyntax.SemanticModel(context);
        return semanticModel.GetTypeInfo(typeSyntax).Type!.IsEntityType(semanticModel.Compilation);
    }

    public static bool IsCollectionType(this TypeSyntax typeSyntax, GeneratorSyntaxContext context)
    {
        SemanticModel semanticModel = typeSyntax.SemanticModel(context);
        return semanticModel.GetTypeInfo(typeSyntax).Type!.IsCollectionType();
    }

    public static TypeSyntax RewriteType(this TypeSyntax type, GeneratorSyntaxContext context, Func<TypeSyntax, TypeSyntax> rewrite) =>
        type switch
        {
            NullableTypeSyntax nullableTypeSyntax =>
                nullableTypeSyntax.WithElementType(nullableTypeSyntax.ElementType.RewriteType(context, rewrite)),
            ArrayTypeSyntax arrayTypeSyntax =>
                arrayTypeSyntax.WithElementType(arrayTypeSyntax.ElementType.RewriteType(context, rewrite)),
            GenericNameSyntax genericNameSyntax =>
                genericNameSyntax.WithTypeArgumentList( // not AddTypeArgumentListArguments: we have to replace the original list
                    SyntaxFactory.TypeArgumentList(
                        SyntaxFactory.SeparatedList(
                            genericNameSyntax.TypeArgumentList.Arguments.Select(syntax => syntax.RewriteType(context, rewrite))
                        )
                    )
                ),
            _ =>
                type.IsEntityType(context) ? rewrite(type) : type
        };
}
