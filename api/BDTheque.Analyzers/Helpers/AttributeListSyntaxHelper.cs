namespace BDTheque.Analyzers.Helpers;

using System.CodeDom.Compiler;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class AttributeListSyntaxHelper
{
    public static AttributeListSyntax GeneratedAttributeList<T>() =>
        SyntaxFactory.AttributeList(
            SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Attribute(
                    SyntaxFactory.IdentifierName("global::" + typeof(GeneratedCodeAttribute).FullName),
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
        );
}
