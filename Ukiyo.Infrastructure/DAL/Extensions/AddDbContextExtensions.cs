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

        public static IUkiyoBuilder AddDbContext(this IUkiyoBuilder builder, string configurationSectionName = SectionName)
        {
            var options = builder.GetOptions<DatabaseOptions>(configurationSectionName);
            return builder.AddDbContext(options);
        }
        
        public static IUkiyoBuilder AddDbContext(this IUkiyoBuilder builder, DatabaseOptions databaseOptions)
        {
            builder.Services.AddDbContextPoolWithConnectionString<AppDbContext>(databaseOptions.GetApplicationConnectionString());
            return builder;
        }
        
        public static void AddDbContextPoolWithConnectionString<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
        {
            services.AddDbContextPool<TDbContext>(options =>
            {
#if DEBUG
                options.EnableSensitiveDataLogging();
#endif
                options.UseNpgsql(connectionString, builder => builder.EnableRetryOnFailure(3));
            });
        }
    }
}