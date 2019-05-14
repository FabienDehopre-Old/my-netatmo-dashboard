using Boxed.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Netatmo.Dashboard.Api.Constants;
using Netatmo.Dashboard.Api.Options;
using Netatmo.Dashboard.Core.Options;
using Netatmo.Dashboard.Tasks;
using System;
using System.Linq;

namespace Netatmo.Dashboard.Api
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application) =>
            application
                // When a database error occurs, displays a detailed error page with full diagnostic information. It is
                // unsafe to use this in production. Uncomment this if using a database.
                // .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
                // When an error occurs, displays a detailed error page with full diagnostic information.
                // See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
                .UseDeveloperExceptionPage();

        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            var cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptions>()
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault();
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            context.Context.ApplyCacheProfile(cacheProfile);
                        },
                    });
        }

        public static IApplicationBuilder UseHangfire(this IApplicationBuilder application, IHostingEnvironment hostingEnvironment)
        {
            var auth0Options = application.ApplicationServices.GetRequiredService<IOptions<Auth0Options>>().Value;
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(application.ApplicationServices));
            return application
                .UseIfElse(
                    hostingEnvironment.IsDevelopment(),
                    x => x.UseHangfireDashboard(options: new DashboardOptions { AppPath = "https://localhost:4200" }),
                    x => x.UseHangfireDashboard(options: new DashboardOptions
                    {
                        Authorization = new[] { new AuthorizationFilter($"https://{auth0Options.Domain}") },
                        AppPath = "TODO"
                    }))
                .UseHangfireServer();
        }
    }
}
