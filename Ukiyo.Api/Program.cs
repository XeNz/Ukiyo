using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukiyo.Infrastructure.DAL.Extensions;
using Ukiyo.Infrastructure.DAL.Options;
using Ukiyo.Infrastructure.Ioc;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webhost = CreateHostBuilder(args);
            var build = webhost.Build();
            await build.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var dbOptions = new DatabaseOptions();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services
                            .AddControllers();

                        services.AddUkiyo()
                            .AddDbContext(dbOptions)
                            .Build();
                    });
                    builder.Configure(app =>
                    {
                        app.UseHttpsRedirection();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
                    });
                })
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("appsettings.json");

                    builder.Build()
                        .GetSection("DataAccess:ConnectionString")
                        .Bind(dbOptions);
                });
        }
    }
}