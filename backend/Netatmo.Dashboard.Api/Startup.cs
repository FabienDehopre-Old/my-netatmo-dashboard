using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netatmo.Dashboard.Api.GraphQL;
using Netatmo.Dashboard.Api.Hangfire;
using Netatmo.Dashboard.Api.Helpers;
using Netatmo.Dashboard.Api.Options;
using Netatmo.Dashboard.Api.Repositories;
using System;
using System.Net.Http;

namespace Netatmo.Dashboard.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NetatmoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddCors(options => 
            {
                var corsOptions = Configuration.GetSection("Cors").Get<CorsOptions>();
                options.AddDefaultPolicy(
                    builder => builder.AllowCredentials()
                                      .WithOrigins(corsOptions.AllowedOrigins)
                                      .SetIsOriginAllowedToAllowWildcardSubdomains()
                                      .WithMethods(corsOptions.AllowedMethods)
                                      .WithHeaders(corsOptions.AllowedHeaders)
                );
            });
            services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("Default")));

            var auth0Options = Configuration.GetSection("Auth0").Get<Auth0Options>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{auth0Options.Domain}/";
                options.Audience = auth0Options.ApiIdentifier;
            });

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("read:values", policy => policy.Requirements.Add(new HasScopeRequirement("read:values", $"https://{auth0Options.Domain}/")));
            //});

            services.Configure<NetatmoOptions>(Configuration.GetSection("Netatmo"));
            services.Configure<Auth0Options>(Configuration.GetSection("Auth0"));

            services.AddSingleton<HttpClient>();
            services.AddTransient<NetatmoTasks>();
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddSingleton<ContextServiceLocator>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<IStationRepository, StationRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IDashboardDataRepository, DashboardDataRepository>();
            services.AddSingleton<NetatmoQuery>();
            services.AddSingleton<StationType>();
            services.AddSingleton<CountryType>();
            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new NetatmoSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

#if DEBUG
            app.UseGraphiQl();
#endif

            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(serviceProvider));
            app.UseHangfireDashboard(options: new DashboardOptions
            {
#if !DEBUG
                Authorization = new[] { new AuthorizationFilter($"https://{Configuration["Auth0:Domain"]}/") },
#endif
                AppPath = env.IsDevelopment() ? "https://localhost:4200" : "TODO"
            });
            app.UseHangfireServer();

            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
