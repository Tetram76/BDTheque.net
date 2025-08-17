// ReSharper disable once CheckNamespace
namespace Microsoft.CodeAnalysis.CSharp.Syntax;

using BDTheque.Analyzers;
using BDTheque.Analyzers.Helpers;

public static class InterfaceDeclarationSyntaxExtensions
{
    public static InterfaceDeclarationSyntax AddGeneratedAttribute<T>(this InterfaceDeclarationSyntax interfaceDeclarationSyntax) =>
        interfaceDeclarationSyntax.AddAttributeLists(AttributeListSyntaxHelper.GeneratedAttributeList<T>());

    public static InterfaceDeclarationSyntax AddApplyToMethod(this InterfaceDeclarationSyntax interfaceDeclarationSyntax, GeneratorSyntaxContext context, ClassDeclarationSyntax classDeclaration)
    {
        TypeSyntax entityTypeSyntax = SyntaxFactory.ParseTypeName(classDeclaration.Identifier.Text);
        List<ParameterSyntax> methodParameters = [SyntaxFactory.Parameter(SyntaxFactory.Identifier("entity")).WithType(entityTypeSyntax)];
        var asyncMethod = false;

        MethodDeclarationSyntax applyToMethod = SyntaxFactory
            .MethodDeclaration(entityTypeSyntax, "ApplyTo")
            .AddModifiers(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword)
            )
            .WithBody(
                SyntaxFactory.Block(
                    classDeclaration.MutableProperties(context)
                        .Select(
                            property =>
                            {
                                var getterExpression = false;
                                if (property.Type.IsEntityType(context))
                                {
                                    TypeSyntax? scalarType = property.GetMutationType(context);
                                    TypeSyntax inputType = property.Type.RewriteType(context, syntax => SyntaxFactory.IdentifierName(scalarType == null ? syntax + WellKnownDefinitions.NestedTypeSuffix : "ushort"));

                                    methodParameters.Add(
                                        SyntaxFactory
                                            .Parameter(SyntaxFactory.Identifier("get" + property.Identifier.Text))
                                            .WithType(SyntaxFactory.GenericName("Func").AddTypeArgumentListArguments(inputType, SyntaxFactory.GenericName("Task").AddTypeArgumentListArguments(property.Type)))
                                    );
                                    getterExpression = true;
                                    asyncMethod = true;
                                }

                                return property.BuildApplyTest(getterExpression);
                            }
                        )
                        .Cast<StatementSyntax>()
                        .Concat(
                            [
                                SyntaxFactory.ReturnStatement(SyntaxFactory.IdentifierName("entity"))
                            ]
                        )
                )
            )
            .AddParameterListParameters(methodParameters.ToArray());

#pragma warning disable S2583 // false positive
        if (asyncMethod)
#pragma warning restore S2583
            applyToMethod = applyToMethod
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.AsyncKeyword))
                .WithReturnType(SyntaxFactory.GenericName("Task").AddTypeArgumentListArguments(applyToMethod.ReturnType));

        return interfaceDeclarationSyntax.AddMembers(applyToMethod);
    }
}
