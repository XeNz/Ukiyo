using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.DAL.Options;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Infrastructure.DAL.Extensions
{
    public static class AddDbContextExtensions
    {
        private const string SectionName = "dataaccess";

        public static IUkiyoBuilder AddDbContext<TDbContext>(this IUkiyoBuilder builder, string configurationSectionName = SectionName) where TDbContext : DbContext
        {
            var options = builder.GetOptions<DatabaseOptions>(configurationSectionName);
            return builder.AddDbContext<TDbContext>(options);
        }

        public static IUkiyoBuilder AddDbContext<TDbContext>(this IUkiyoBuilder builder, DatabaseOptions databaseOptions) where TDbContext : DbContext
        {
            builder.Services.AddDbContextPoolWithConnectionString<TDbContext>(databaseOptions.GetApplicationConnectionString());
            return builder;
        }

        public static void AddDbContextPoolWithConnectionString<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContextPool<TDbContext>(options =>
            {
#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
                options.UseNpgsql(connectionString, builder =>
                {
                    builder.MigrationsAssembly(typeof(AddDbContextExtensions).Assembly.FullName);
                    builder.EnableRetryOnFailure(3);
                });
            });
        }
    }
}