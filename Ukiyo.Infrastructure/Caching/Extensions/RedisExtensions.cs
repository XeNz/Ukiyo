using System;
using Ukiyo.Infrastructure.Ioc;

namespace Ukiyo.Infrastructure.Caching
{
    public static class RedisExtensions
    {
        private const string SectionName = "redis";
        private const string RegistryName = "persistence.redis";

        public static IUkiyoBuilder AddRedis(this IUkiyoBuilder builder, string sectionName = SectionName)
        {
            var options = builder.GetOptions<RedisOptions>(sectionName);
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