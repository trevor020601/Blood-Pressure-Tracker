using Microsoft.Extensions.DependencyInjection;

namespace SharedLibrary.Attributes;

/// <summary>
/// 
/// The InjectDependency attribute must be attached to an interface, so that on application startup,
/// the assembly is scanned via reflection and automatically registered into the DI container
/// 
/// Example:
///     
///     [InjectDependency(ServiceLifetime.Singleton)]
///     public interface IFoo 
///     {
///         void Bar();
///     }
///     
///     public class Foo : IFoo 
///     {
///         public void Bar() {
///             /* Implementation */
///         }
///     }
///     
/// </summary>
[AttributeUsage(AttributeTargets.Interface)]
public class InjectDependencyAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public InjectDependencyAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
