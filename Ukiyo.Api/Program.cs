using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukiyo.Infrastructure.Caching.Extensions;
using Ukiyo.Infrastructure.CQRS.Extensions;
using Ukiyo.Infrastructure.DAL;
using Ukiyo.Infrastructure.DAL.Extensions;
using Ukiyo.Infrastructure.Ioc.Extensions;
using Ukiyo.Infrastructure.Mapping;
using Utf8Json.AspNetCoreMvcFormatter;
using Utf8Json.Resolvers;

namespace Ukiyo.Api
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.AddMvc().AddMvcOptions(options =>
                        {
                            options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
                            options.InputFormatters.RemoveType(typeof(SystemTextJsonInputFormatter));

                            var resolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.AllowPrivateCamelCase);

                            options.OutputFormatters.Add(new JsonOutputFormatter(resolver));
                            options.InputFormatters.Add(new JsonInputFormatter(resolver));
                        });

                        services.AddUkiyo()
                            .AddDbContext<AppDbContext>()
                            .AddDbContext<IdentityDbContext>()
                            .AddUnitOfWork()
                            .AddRedis()
                            .AddAutoMapper(typeof(Program))
                            .AddEventHandlers()
                            .AddQueryHandlers()
                            .AddEventHandlers()
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