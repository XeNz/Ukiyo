namespace Ukiyo.Infrastructure.Caching.Options
{
    public class RedisOptions
    {
        public string ConnectionString { get; set; } = "localhost";
        public string Instance { get; set; }
    }
}