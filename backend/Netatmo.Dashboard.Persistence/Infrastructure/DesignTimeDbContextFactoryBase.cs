using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Netatmo.Dashboard.Persistence.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "NetatmoDatabase";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Presentation", "Netatmo.Dashboard.Api");
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            var connectionString = configuration.GetConnectionString(ConnectionStringName);
            return Create(connectionString);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");
            var optionBuilder = new DbContextOptionsBuilder<TContext>();
            optionBuilder.UseSqlServer(connectionString);
            return CreateNewInstance(optionBuilder.Options);
        }
    }
}
