using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Netatmo.Dashboard.Api
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            );
        }

        public TContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = AppContext.BaseDirectory;
            return Create(basePath, environmentName);
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            var connStr = config.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new InvalidOperationException("Could not find a connection string named 'Default'.");
            }

            return Create(connStr);
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException($"{nameof(connectionString)} is null or empty.", nameof(connectionString));
            }

            var optionBuilder = new DbContextOptionsBuilder<TContext>();
            Console.WriteLine("MyDesignTimeDbContextFactory.Create(string): Connection string: {0}", connectionString);
            optionBuilder.UseSqlServer(connectionString);
            var options = optionBuilder.Options;
            return CreateNewInstance(options);
        }
    }
}
