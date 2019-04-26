using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Core.Data;
using Netatmo.Dashboard.Data;
using Netatmo.Dashboard.Data.Repositories;
using Netatmo.Dashboard.GraphQL.Helpers;
using Netatmo.Dashboard.GraphQL.Schemas;

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
        /// <summary>
        /// Add project data repositories.
        /// </summary>
        public static IServiceCollection AddProjectRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<IStationRepository, StationRepository>()
                .AddSingleton<ICountryRepository, CountryRepository>()
                .AddSingleton<IDeviceRepository, DeviceRepository>()
                .AddSingleton<IDashboardDataRepository, DashboardDataRepository>()
                .AddSingleton<ContextServiceLocator>();

        /// <summary>
        /// Add project GraphQL schema and web socket types.
        /// </summary>
        public static IServiceCollection AddProjectSchema(this IServiceCollection services) =>
            services
                .AddSingleton<MainSchema>();

        public static IServiceCollection AddProjectDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddDbContext<NetatmoDbContext>(
                    options => options.UseSqlServer(configuration.GetConnectionString("Default"))) ;
    }
}