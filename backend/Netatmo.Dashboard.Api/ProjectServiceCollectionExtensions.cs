using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Application.Infrastructure;
using Netatmo.Dashboard.Application.Interfaces;
using Netatmo.Dashboard.Application.Users.Queries;
using Netatmo.Dashboard.Persistence;
using System.Reflection;

namespace Netatmo.Dashboard.Api
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<INetatmoDbContext, NetatmoDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("NetatmoDatabase")));

        public static IServiceCollection AddProjectMediatR(this IServiceCollection service) =>
            service
                .AddMediatR(typeof(GetUserByUidQuery).GetTypeInfo().Assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>))
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        public static IServiceCollection AddProjectSchema(this IServiceCollection services) =>
            services;
                // .AddSingleton<MainSchema>();
    }
}
