using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Persistence.Utils
{
    public class ContextDbFactory : IDesignTimeDbContextFactory<ContextDb>
    {
        public ContextDb CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Server");

            var config = new ConfigurationBuilder()
                   .SetBasePath(basePath)
                   .AddJsonFile("AppSettings.json", optional: false)
                   .Build();

            var connectionString = config.GetConnectionString("bd.url");

            var optionsBuilder = new DbContextOptionsBuilder<ContextDb>();
            optionsBuilder.UseSqlite(connectionString);
            return new ContextDb(optionsBuilder.Options);
        }
    }
}
