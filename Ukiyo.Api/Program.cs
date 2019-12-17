using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukiyo.Infrastructure.Caching.Extensions;
using Ukiyo.Infrastructure.DAL.Extensions;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Api
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                //.ConfigureAppConfiguration(builder => { })
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddControllers();

                        services.AddUkiyo()
                            .AddDbContext()
                            .AddUnitOfWork()
                            .AddRedis()
                            .Build();
                    });

                    builder.Configure(app =>
                    {
                        app.UseHttpsRedirection();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                    });
                });
        }
    }
}