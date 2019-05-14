using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Netatmo.Dashboard.Application.Users.Queries;
using AutoMapper;
using Netatmo.Dashboard.Application.Infrastructure.AutoMapper;
using System.Reflection;
using CorrelationId;
using Netatmo.Dashboard.Api.Constants;
using Boxed.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;

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
                .AddCustomMvcOptions()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetUserByUidQueryValidator>())
            .Services
            .AddProjectDbContext(configuration)
            .AddProjectMediatR()
            .AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly)
            .ConfigureApiBehavior()
            .BuildServiceProvider();

        public void Configure(IApplicationBuilder application) =>
            application
                // Pass a GUID in a X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier.
                // UpdateTraceIdentifier must be false due to a bug. See https://github.com/aspnet/AspNetCore/issues/5144
                .UseCorrelationId(new CorrelationIdOptions { UpdateTraceIdentifier = false })
                .UseForwardedHeaders()
                .UseResponseCompression()
                .UseCors(CorsPolicyName.AllowAngularApp)
                .UseIfElse(
                    hostingEnvironment.IsDevelopment(),
                    x => x.UseDeveloperErrorPages(),
                    x => x.UseHsts())
                .UseHealthChecks("/status")
                .UseHealthChecks("/status/self", new HealthCheckOptions { Predicate = _ => false })
                .UseStaticFilesWithCacheControl()
                .UseWebSockets()
                // Use the GraphQL subscriptions in the specified schema and make them available at /graphql.
                // .UseGraphQLWebSockets<MainSchema>()
                // Use the specified GraphQL schema and make them available at /graphql.
                // .UseGraphQL<MainSchema>()
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
