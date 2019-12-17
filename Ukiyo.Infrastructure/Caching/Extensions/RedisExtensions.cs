using System;
using Microsoft.Extensions.DependencyInjection;
using Ukiyo.Infrastructure.Caching.Builders;
using Ukiyo.Infrastructure.Caching.Options;
using Ukiyo.Infrastructure.Ioc.Builders;
using Ukiyo.Infrastructure.Ioc.Extensions;

namespace Ukiyo.Infrastructure.Caching.Extensions
{
    public static class RedisExtensions
    {
        private const string SectionName = "redis";
        private const string RegistryName = "persistence.redis";

        public static IUkiyoBuilder AddRedis(this IUkiyoBuilder builder, string configurationSectionName = SectionName)
        {
            var options = builder.GetOptions<RedisOptions>(configurationSectionName);
            return builder.AddRedis(options);
        }

        public static IUkiyoBuilder AddRedis(this IUkiyoBuilder builder,
            Func<IRedisOptionsBuilder, IRedisOptionsBuilder> buildOptions)
        {
            var options = buildOptions(new RedisOptionsBuilder()).Build();
            return builder.AddRedis(options);
        }

        public static IUkiyoBuilder AddRedis(this IUkiyoBuilder builder, RedisOptions options)
        {
            if (!builder.TryRegister(RegistryName))
            {
                return builder;
            }

            builder.Services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = options.ConnectionString;
                o.InstanceName = options.Instance;
            });

            return builder;
        }
    }
}