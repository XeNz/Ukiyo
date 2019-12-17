using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ukiyo.Infrastructure.DAL
{
    public static class AddDbContextExtensions
    {
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