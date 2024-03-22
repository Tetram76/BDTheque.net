namespace BDTheque.Analyzers;

using System.Diagnostics.CodeAnalysis;

#pragma warning disable S3218
[SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
public static class WellKnownDefinitions
{
    public const string CreateInputTypeSuffix = "CreateInput";
    public const string UpdateInputTypeSuffix = "UpdateInput";
    public const string NestedTypeSuffix = "NestedInput";

    public const string InputsNamespaceName = "BDTheque.Model.Inputs";

    public static class HotChocolate
    {
        private const string Namespace = nameof(HotChocolate);

        public static class Types
        {
            public const string Namespace = $"{HotChocolate.Namespace}.{nameof(Types)}";

            public const string ObjectTypeAttribute = $"{Namespace}.ObjectTypeAttribute";
            public const string MutationTypeAttribute = $"{Namespace}.MutationTypeAttribute";
            public const string IdAttribute = $"{Namespace}.Relay.IDAttribute";
        }

        public const string OptionalType = $"{Namespace}.Optional";
    }

    public static class BDTheque
    {
        private const string Namespace = nameof(BDTheque);

        public static class Model
        {
            private const string Namespace = $"{BDTheque.Namespace}.{nameof(Model)}";

            public const string GraphQLReadOnlyAttribute = $"{Namespace}.Attributes.GraphQLReadOnlyAttribute";
            public const string PropertyMutationTypeAttribute = $"{Namespace}.Attributes.MutationScalarTypeAttribute`1";
            public const string StaticEntityAttribute = $"{Namespace}.Attributes.StaticEntityAttribute";
            public const string UniqueIdEntity = $"{Namespace}.Entities.Abstract.UniqueIdEntity";
        }

        public const string DBContext = $"{Namespace}.Data.Context.BDThequeContext";

        public const string MutationEntityAttribute = $"{Namespace}.GraphQL.Attributes.MutationEntityAttribute`1";
    }

    public static class System
    {
        private const string Namespace = nameof(System);

        public static class Collections
        {
            private const string Namespace = $"{System.Namespace}.{nameof(Collections)}";

            public static class Generic
            {
                public const string Namespace = $"{Collections.Namespace}.{nameof(Generic)}";
            }
        }
    }
}
#pragma warning restore S3218
