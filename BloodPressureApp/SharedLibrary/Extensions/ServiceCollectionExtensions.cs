﻿using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Attributes;
using System.Reflection;

namespace SharedLibrary.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServicesByAttribute(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            var interfaces = assembly.GetTypes()
                                                    .Where(t => t.IsInterface &&
                                                                    t.GetCustomAttribute<InjectDependencyAttribute>() != null);

            foreach (var interFace in interfaces)
            {
                var attribute = interFace.GetCustomAttribute<InjectDependencyAttribute>();

                var implementation = assembly.GetTypes()
                                                  .FirstOrDefault(t => t.IsClass &&
                                                                           !t.IsAbstract &&
                                                                           interFace.IsAssignableFrom(t));

                if (implementation == null)
                {
                    continue;
                }

                // TODO: Maybe throw an exception if the attribute doesn't exist or figure out some other graceful way to handle this case...
                switch (attribute!.Lifetime)
                {
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interFace, implementation);
                        break;
                    case ServiceLifetime.Transient:
                        services.AddTransient(interFace, implementation);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(interFace, implementation);
                        break;
                }
            }
        }

        return services;
    }
}
