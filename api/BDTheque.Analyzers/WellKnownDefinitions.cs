namespace BDTheque.Analyzers;

using System.Diagnostics.CodeAnalysis;

#pragma warning disable S3218 // shadowed names are not a problem here
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

            public static class Attributes
            {
                private const string Namespace = $"{Model.Namespace}.{nameof(Attributes)}";

                public const string GraphQLReadOnlyAttribute = $"{Namespace}.GraphQLReadOnlyAttribute";
                public const string PropertyMutationTypeAttributeTypeName = $"{Namespace}.MutationScalarTypeAttribute";
                public const string PropertyMutationTypeAttributeMetadataName = $"{PropertyMutationTypeAttributeTypeName}`1";
                public const string StaticEntityAttribute = $"{Namespace}.StaticEntityAttribute";
            }

            public static class Entities
            {
                private const string Namespace = $"{Model.Namespace}.{nameof(Entities)}";

                public const string BaseEntity = $"{Namespace}.Abstract.BaseEntity";
            }
        }

        public static class Data
        {
            private const string Namespace = $"{BDTheque.Namespace}.{nameof(Data)}";

            public const string DbContext = $"{Namespace}.Context.BDThequeContext";

            public static class Repositories
            {
                private const string Namespace = $"{Data.Namespace}.{nameof(Repositories)}";

                private const string EntityRepositoryTypeName = $"{Namespace}.EntityRepository";
                public const string EntityRepositoryMetadataName = $"{EntityRepositoryTypeName}`2";

                public static class Interfaces
                {
                    private const string Namespace = $"{Repositories.Namespace}.{nameof(Interfaces)}";

                    public const string EntityRepositoryTypeName = $"{Namespace}.IEntityRepository";
                    public const string EntityRepositoryMetadataName = $"{EntityRepositoryTypeName}`2";
                }
            }

            public static class Attributes
            {
                private const string Namespace = $"{Data.Namespace}.{nameof(Attributes)}";

                public const string EntityRepositoryAttribute = $"{Namespace}.EntityRepositoryAttribute";
            }
        }

        public static class GraphQL
        {
            private const string Namespace = $"{BDTheque.Namespace}.{nameof(GraphQL)}";

            public static class Attributes
            {
                private const string Namespace = $"{GraphQL.Namespace}.{nameof(Attributes)}";

                private const string MutationEntityAttributeTypeName = $"{Namespace}.MutationEntityAttribute";
                public const string MutationEntityAttributeMetadataName = $"{MutationEntityAttributeTypeName}`1";
            }
        }
    }

    public static class System
    {
        private const string Namespace = nameof(System);

        public const string ServiceProvider = $"{Namespace}.IServiceProvider";

        public static class Collections
        {
            private const string Namespace = $"{System.Namespace}.{nameof(Collections)}";

            public static class Generic
            {
                public const string Namespace = $"{Collections.Namespace}.{nameof(Generic)}";
            }
        }
    }

    public static class Microsoft
    {
        private const string Namespace = nameof(Microsoft);

        public static class Extensions
        {
            private const string Namespace = $"{Microsoft.Namespace}.{nameof(Extensions)}";

            public static class DependencyInjection
            {
                public const string Namespace = $"{Extensions.Namespace}.{nameof(DependencyInjection)}";
            }
        }
    }
}
#pragma warning restore S3218
