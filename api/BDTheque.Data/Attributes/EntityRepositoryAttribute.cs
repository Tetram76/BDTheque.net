namespace BDTheque.Data.Attributes;

using Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class)]
public class EntityRepositoryAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
    public ServiceLifetime Lifetime { get; } = lifetime;
}
