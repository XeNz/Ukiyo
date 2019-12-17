using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Initializers;
using Ukiyo.Infrastructure.Ioc.Options;
using Ukiyo.Infrastructure.Ioc.ServicesIdentification;

namespace Ukiyo.Infrastructure.Ioc.Extensions
{
    public static class UkiyoExtensions
    {
        public static IUkiyoBuilder AddUkiyo(this IServiceCollection services, string appOptionsSectionName = "app")
        {
            var builder = UkiyoBuilder.Create(services);
            var options = builder.GetOptions<AppOptions>(appOptionsSectionName);
            builder.Services.AddMemoryCache();
            services.AddSingleton(options);
            services.AddSingleton<IServiceId, ServiceId>();
            if (options.DisplayBanner && !string.IsNullOrWhiteSpace(options.Name))
            {
                Console.WriteLine(Figgle.FiggleFonts.Doom.Render($"{options.Name} {options.Version}"));
            }

            return builder;
        }

        public static IApplicationBuilder UseUkiyo(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<IStartupInitializer>();
                if (initializer is null)
                {
                    throw new InvalidOperationException("Startup initializer was not found.");
                }

                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }

        public static TModel GetOptions<TModel>(this IConfiguration configuration, string sectionName)
            where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(sectionName).Bind(model);
            return model;
        }

        public static TModel GetOptions<TModel>(this IUkiyoBuilder builder, string settingsSectionName)
            where TModel : new()
        {
            using (var serviceProvider = builder.Services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                return configuration.GetOptions<TModel>(settingsSectionName);
            }
        }

        public static IApplicationBuilder UseInitializers(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetService<IStartupInitializer>();
                if (initializer is null)
                {
                    throw new InvalidOperationException("Startup initializer was not found.");
                }

                Task.Run(() => initializer.InitializeAsync()).GetAwaiter().GetResult();
            }

            return app;
        }

        public static string Underscore(this string value)
            => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()));
    }
}