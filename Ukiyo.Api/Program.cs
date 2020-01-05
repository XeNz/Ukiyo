using System.Threading.Tasks;
using Figgle;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ukiyo.Api.CQRS;
using Ukiyo.Api.CQRS.Commands.Posts;
using Ukiyo.Api.CQRS.Queries;
using Ukiyo.Api.CQRS.Queries.Posts;
using Ukiyo.Api.Dtos;
using Ukiyo.Core;
using Ukiyo.Infrastructure.Caching.Extensions;
using Ukiyo.Infrastructure.CQRS.Extensions;
using Ukiyo.Infrastructure.DAL.Extensions;
using Ukiyo.Infrastructure.Ioc.Extensions;
using Ukiyo.Infrastructure.Ioc.Options;
using Ukiyo.Infrastructure.Mapping;
using Ukiyo.Infrastructure.WebApi;
using Ukiyo.Infrastructure.WebApi.CQRS;
using Ukiyo.Infrastructure.WebApi.Swagger;

namespace Ukiyo.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.ConfigureServices(services => services
                        .AddUkiyo()
                        .AddWebApi(coreBuilder => { })
                        .AddDbContext<AppDbContext>()
                        //.AddDbContext<IdentityDbContext>()
                        .AddRedis()
                        .AddAutoMapper(typeof(Program))
                        .AddEventHandlers()
                        .AddQueryHandlers()
                        .AddEventHandlers()
                        .AddInMemoryQueryDispatcher()
                        .AddSwaggerDocs()
                        .AddWebApiSwaggerDocs()
                        .Build());

                    builder.Configure(app => app
                        .UseHttpsRedirection()
                        .UseRouting()
                        .UseAuthorization()
                        .UseSwaggerDocs()
                        .UseDispatcherEndpoints(endpoints => endpoints
                            .Get("", ctx =>
                            {
                                var appOptions = ctx.RequestServices.GetService<AppOptions>();
                                return ctx.Response.WriteAsync(FiggleFonts.Doom.Render($"{appOptions.Name} {appOptions.Version}"));
                            })
                            .Get<GetPostsQuery, PostCollectionDto>("posts")
                            .Get<GetPostByIdQuery, PostDto>("posts/{postId}")
                            .Post<CreatePostCommand>("posts", afterDispatch: (cmd, ctx) => ctx.Response.Created($"vehicles/{cmd.PostId}"))
                            .Put<UpdatePostCommand>("posts/{postId}")
                            .Delete<DeletePostCommand>("posts/{postId}")
                        ));
                })
                .Build()
                .RunAsync();
        }
    }
}