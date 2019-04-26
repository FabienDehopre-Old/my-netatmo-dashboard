using Boxed.AspNetCore;
using CorrelationId;
using GraphiQl;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Api.Constants;
using Netatmo.Dashboard.Core.Options;
using Netatmo.Dashboard.GraphQL.Schemas;
using System;

namespace Netatmo.Dashboard.Api
{
    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) =>
            services
            .AddCorrelationIdFluent()
            .AddCustomCaching()
            .AddCustomOptions(configuration)
            .AddCustomRouting()
            .AddCustomResponseCompression()
            .AddCustomStrictTransportSecurity()
            .AddCustomHealthChecks()
            .AddHttpContextAccessor()
            .AddMvcCore()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddAuthorization()
                .AddJsonFormatters()
                .AddCustomJsonOptions(hostingEnvironment)
                .AddCustomCors(configuration)
                .AddCustomMvcOptions(hostingEnvironment)
            .Services
            .AddCustomGraphQL(hostingEnvironment)
            .AddCustomGraphQLAuthorization()
            .AddProjectRepositories()
            .AddProjectSchema()
            .AddProjectDbContext(configuration)
            .AddHangfire(config => config.UseSqlServerStorage(configuration.GetConnectionString("Default")))
            .AddCustomAuthentication(configuration.GetSection("Auth0").Get<Auth0Options>())
            .BuildServiceProvider();

        public void Configure(IApplicationBuilder application) =>
            application
                // Pass a GUID in a X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier.
                // UpdateTraceIdentifier must be false due to a bug. See https://github.com/aspnet/AspNetCore/issues/5144
                .UseCorrelationId(new CorrelationIdOptions { UpdateTraceIdentifier = false })
                .UseForwardedHeaders()
                .UseResponseCompression()
                .UseCors(CorsPolicyName.AllowAny)
                .UseIfElse(
                    hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperErrorPages(),
                    x => x.UseHsts())
                .UseHealthChecks("/status")
                .UseHealthChecks("/status/self", new HealthCheckOptions { Predicate = _ => false })
                .UseStaticFilesWithCacheControl()
                .UseWebSockets()
                // Use the GraphQL subscriptions in the specified schema and make them available at /graphql.
                .UseGraphQLWebSockets<MainSchema>()
                // Use the specified GraphQL schema and make them available at /graphql.
                .UseGraphQL<MainSchema>()
                .UseIf(
                    hostingEnvironment.IsDevelopment(),
                    x => x
                        // Add the GraphQL Playground UI to try out the GraphQL API at /.
                        .UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/playground" })
                        // Add the GraphQL Voyager UI to let you navigate your GraphQL API as a spider graph at /voyager.
                        .UseGraphQLVoyager(new GraphQLVoyagerOptions() { Path = "/voyager" }))
                .UseHangfire(hostingEnvironment)
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseMvc();
    }
}
