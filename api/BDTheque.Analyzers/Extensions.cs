namespace BDTheque.Analyzers;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class Extensions
{
    public static ClassDeclarationSyntax AddGeneratedAttribute<T>(this ClassDeclarationSyntax classDeclarationSyntax) =>
        classDeclarationSyntax.AddAttributeLists(
            SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(
                        SyntaxFactory.IdentifierName("global::" + typeof(System.CodeDom.Compiler.GeneratedCodeAttribute).FullName),
                        SyntaxFactory.AttributeArgumentList(
                            SyntaxFactory.SeparatedList(
                                [
                                    SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(typeof(T).FullName!))),
                                    SyntaxFactory.AttributeArgument(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(typeof(T).Assembly.GetName().Version.ToString())))
                                ]
                            )
                        )
                    )
                )
            )
        );
}
