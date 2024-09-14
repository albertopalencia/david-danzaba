// ***********************************************************************
// Assembly         : RealState.Infrastructure
// Author           : Usuario
// Created          : 09-11-2024
//
// Last Modified By : Usuario
// Last Modified On : 09-12-2024
// ***********************************************************************
// <copyright file="AutoLoadServices.cs" company="RealState.Infrastructure">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using RealState.Application.Ports;
using RealState.Domain.Common;
using RealState.Infrastructure.Adapters;
using RealState.Infrastructure.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace RealState.Infrastructure.Extensions;

/// <summary>
/// Class AutoLoadServices.
/// </summary>
public static class AutoLoadServices
{
    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        var _services = AppDomain.CurrentDomain.GetAssemblies()
              .Where(assembly => (assembly.FullName is null) || assembly.FullName.Contains("Domain", StringComparison.OrdinalIgnoreCase))
              .SelectMany(assembly => assembly.GetTypes())
              .Where(type => type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(DomainServiceAttribute)));

        var repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly => (assembly.FullName is null) || assembly.FullName.Contains("Infrastructure", StringComparison.OrdinalIgnoreCase))
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(RepositoryAttribute)));

        foreach (var service in _services)
        {
            services.AddTransient(service);
        }

        foreach (var repository in repositories)
        {
            Type typeInterface = repository.GetInterfaces().Single();
            services.AddTransient(typeInterface, repository);
        }

        return services;
    }
}
