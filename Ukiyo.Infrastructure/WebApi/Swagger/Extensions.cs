using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Extensions;
using Ukiyo.Infrastructure.WebApi.Swagger.Builders;
using Ukiyo.Infrastructure.WebApi.Swagger.Filters;

namespace Ukiyo.Infrastructure.WebApi.Swagger
{
    public static class Extensions
    {
        private const string SectionName = "swagger";

        public static IUkiyoBuilder AddWebApiSwaggerDocs(this IUkiyoBuilder builder, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            return builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(sectionName));
        }

        public static IUkiyoBuilder AddWebApiSwaggerDocs(this IUkiyoBuilder builder,
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(buildOptions));

        public static IUkiyoBuilder AddWebApiSwaggerDocs(this IUkiyoBuilder builder, SwaggerOptions options)
            => builder.AddWebApiSwaggerDocs(b => b.AddSwaggerDocs(options));

        private static IUkiyoBuilder AddWebApiSwaggerDocs(this IUkiyoBuilder builder, Action<IUkiyoBuilder> registerSwagger)
        {
            registerSwagger(builder);
            builder.Services.AddSwaggerGen(c => c.DocumentFilter<WebApiDocumentFilter>());
            return builder;
        }

        private const string RegistryName = "docs.swagger";

        public static IUkiyoBuilder AddSwaggerDocs(this IUkiyoBuilder builder, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = builder.GetOptions<SwaggerOptions>(sectionName);
            return builder.AddSwaggerDocs(options);
        }

        public static IUkiyoBuilder AddSwaggerDocs(this IUkiyoBuilder builder,
            Func<ISwaggerOptionsBuilder, ISwaggerOptionsBuilder> buildOptions)
        {
            var options = buildOptions(new SwaggerOptionsBuilder()).Build();
            return builder.AddSwaggerDocs(options);
        }

        public static IUkiyoBuilder AddSwaggerDocs(this IUkiyoBuilder builder, SwaggerOptions options)
        {
            if (!options.Enabled || !builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddSingleton(options);
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Name, new OpenApiInfo {Title = options.Title, Version = options.Version});
                if (options.IncludeSecurity)
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            });

            return builder;
        }

        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetService<SwaggerOptions>();
            if (!options.Enabled)
            {
                return builder;
            }

            var routePrefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "swagger" : options.RoutePrefix;

            builder.UseStaticFiles()
                .UseSwagger(c => c.RouteTemplate = routePrefix + "/{documentName}/swagger.json");

            return options.ReDocEnabled
                ? builder.UseReDoc(c =>
                {
                    c.RoutePrefix = routePrefix;
                    c.SpecUrl = $"{options.Name}/swagger.json";
                })
                : builder.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/{routePrefix}/{options.Name}/swagger.json", options.Title);
                    c.RoutePrefix = routePrefix;
                });
        }
    }
}