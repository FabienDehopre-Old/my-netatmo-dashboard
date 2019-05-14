using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;

namespace Netatmo.Dashboard.Api
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                    config.Enrich.FromLogContext()
                          .MinimumLevel.Debug()
                          .WriteTo.Console()
                          .WriteTo.Sentry(sentryOptions =>
                          {
                              sentryOptions.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                              sentryOptions.MinimumEventLevel = LogEventLevel.Warning;
                          })
                )
                .UseSentry()
                .UseStartup<Startup>()
                .Build();
    }
}
