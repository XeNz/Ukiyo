using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukiyo.Api.CQRS;
using Ukiyo.Core;
using Ukiyo.Infrastructure.Caching.Extensions;
using Ukiyo.Infrastructure.CQRS.Extensions;
using Ukiyo.Infrastructure.DAL.Extensions;
using Ukiyo.Infrastructure.Ioc.Extensions;
using Ukiyo.Infrastructure.Ioc.Options;
using Ukiyo.Infrastructure.Mapping;
using Ukiyo.Infrastructure.WebApi;
using Ukiyo.Infrastructure.WebApi.CQRS;

namespace Ukiyo.Api
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(services => services
                        .AddUkiyo()
                        .AddWebApi(coreBuilder => { })
                        .AddDbContext<AppDbContext>()
                        .AddDbContext<IdentityDbContext>()
                        .AddRedis()
                        .AddAutoMapper(typeof(Program))
                        .AddEventHandlers()
                        .AddQueryHandlers()
                        .AddEventHandlers()
                        .AddInMemoryQueryDispatcher()
                        .Build());

                    builder.Configure(app => app
                        .UseHttpsRedirection()
                        .UseRouting()
                        .UseAuthorization()
                        // .UseEndpoints(endpoints => { endpoints.MapControllers(); })
                        .UseDispatcherEndpoints(endpoints => endpoints
                                .Get("", ctx =>
                                {
                                    var appOptions = ctx.RequestServices.GetService<AppOptions>();
                                    return ctx.Response.WriteAsync(Figgle.FiggleFonts.Doom.Render($"{appOptions.Name} {appOptions.Version}"));
                                })
                                .Get<GetPostsQuery, PostCollectionDto>("posts")
                            // .Get<SearchVehicles, PagedResult<VehicleDto>>("vehicles")
                            // .Post<AddVehicle>("vehicles",
                            //     afterDispatch: (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.VehicleId}"))
                            // .Put<UpdateVehicle>("vehicles/{vehicleId}")
                            // .Delete<DeleteVehicle>("vehicles/{vehicleId}")
                        ));
                })
                .Build()
                .RunAsync();
    }
}
