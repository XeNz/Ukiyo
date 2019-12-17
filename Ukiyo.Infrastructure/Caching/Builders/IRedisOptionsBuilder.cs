using Ukiyo.Infrastructure.Caching.Options;

namespace Ukiyo.Infrastructure.Caching.Builders
{
    public interface IRedisOptionsBuilder
    {
        IRedisOptionsBuilder WithConnectionString(string connectionString);
        IRedisOptionsBuilder WithInstance(string instance);
        RedisOptions Build();
    }
}