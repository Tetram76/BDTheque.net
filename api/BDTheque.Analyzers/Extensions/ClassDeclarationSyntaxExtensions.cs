namespace BDTheque.Analyzers.Extensions;

using System.CodeDom.Compiler;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class ClassDeclarationSyntaxExtensions
{
    public static bool IsAnnotatedWithObjectType(this ClassDeclarationSyntax classDeclarationSyntax, GeneratorSyntaxContext context) =>
        classDeclarationSyntax.IsAnnotatedWithAttribute("HotChocolate.Types.ObjectTypeAttribute", context);

    public static IEnumerable<MemberDeclarationSyntax> AllMembers(this ClassDeclarationSyntax classDeclaration, GeneratorSyntaxContext context)
    {
        var members = classDeclaration.Members.ToList();
        if (classDeclaration.SemanticModel(context).GetDeclaredSymbol(classDeclaration) is not { } classSymbol)
            return members;

        var baseTypeSymbol = classSymbol.BaseType;
        while (baseTypeSymbol != null && baseTypeSymbol.SpecialType != SpecialType.System_Object)
        {
            var baseTypeDeclarations = baseTypeSymbol.DeclaringSyntaxReferences.Select(sr => sr.GetSyntax()).OfType<ClassDeclarationSyntax>();
            foreach (var baseTypeDeclaration in baseTypeDeclarations)
            {
                members.AddRange(baseTypeDeclaration.Members);
            }

            baseTypeSymbol = baseTypeSymbol.BaseType;
        }

        return members;
    }

    public static ClassDeclarationSyntax AddGeneratedAttribute<T>(this ClassDeclarationSyntax classDeclarationSyntax) =>
        classDeclarationSyntax.AddAttributeLists(
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
            )
        );

    public static ClassDeclarationSyntax AddImplicitConverter(this ClassDeclarationSyntax classDeclarationSyntax, PropertyDeclarationSyntax? idProperty)
    {
        if (idProperty != null)
        {
            var classNameSyntax = SyntaxFactory.IdentifierName(classDeclarationSyntax.Identifier.Text);
            string parameterName = idProperty.Identifier.Text.ToCamelCase();

            classDeclarationSyntax = classDeclarationSyntax.AddMembers(
                SyntaxFactory
                    .ConversionOperatorDeclaration(SyntaxFactory.Token(SyntaxKind.ImplicitKeyword), classNameSyntax)
                    .AddModifiers(
                        SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                        SyntaxFactory.Token(SyntaxKind.StaticKeyword)
                    )
                    .AddParameterListParameters(
                        SyntaxFactory.Parameter(SyntaxFactory.Identifier(parameterName)).WithType(idProperty.Type)
                    )
                    .WithBody(
                        SyntaxFactory.Block(
                            SyntaxFactory.SingletonList<StatementSyntax>(
                                SyntaxFactory.ReturnStatement(
                                    SyntaxFactory.ObjectCreationExpression(classNameSyntax)
                                        .WithInitializer(
                                            SyntaxFactory.InitializerExpression(SyntaxKind.ObjectInitializerExpression)
                                                .AddExpressions(
                                                    SyntaxFactory.AssignmentExpression(
                                                        SyntaxKind.SimpleAssignmentExpression,
                                                        SyntaxFactory.IdentifierName(idProperty.Identifier.Text),
                                                        SyntaxFactory.IdentifierName(parameterName)
                                                    )
                                                )
                                        )
                                )
                            )
                        )
                    )
            );
        }

        return classDeclarationSyntax;
    }
}
