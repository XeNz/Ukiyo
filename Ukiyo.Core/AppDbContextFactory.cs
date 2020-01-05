using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Ukiyo.Infrastructure.DAL.Options;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Core
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbOptions = configuration.GetOptions<DatabaseOptions>("dataaccess");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(dbOptions.GetConnectionString());

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}