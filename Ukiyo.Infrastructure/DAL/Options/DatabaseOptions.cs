namespace Ukiyo.Infrastructure.DAL.Options
{
    public class DatabaseOptions
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string DbName { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        /// <summary>
        ///     The minimum pool size, the default of 1 is a bit low so we tweak it per environment.
        /// </summary>
        public int MinimumPoolSize { get; set; }

        /// <summary>
        ///     The maximum pool size, make sure you don't exceed max_connections (SELECT * FROM pg_settings WHERE name = 'max_connections';).
        /// </summary>
        public int MaximumPoolSize { get; set; }

        /// <summary>
        ///     When should connections from the pool, that exceed minimum pool size be retired.
        /// </summary>
        public int ConnectionIdleLifeTime { get; set; }

        /// <summary>
        ///     Number of prepared statements to cache.
        /// </summary>
        public int MaxAutoPrepare { get; set; }

        public string GetConnectionString()
        {
            return new ConnectionString(Host, Port, DbName, User, Password).ToString();
        }

        /// <summary>
        ///     The default connection string with a pool based off the dataaccess.json file.
        /// </summary>
        public string GetApplicationConnectionString()
        {
            return GetConnectionStringWithPool(MinimumPoolSize, MaximumPoolSize, ConnectionIdleLifeTime, MaxAutoPrepare);
        }

        /// <summary>
        ///     Since we are distributing the Postgres max_connections (100) between 2 pools, use a fixed pool for hangfire.
        ///     The maximum pool size is fixed on 10, make sure you don't exceed max_connections on the application connection string when adding this pool size.
        /// </summary>
        public string GetHangfireConnectionStringWithFixedPool()
        {
            return GetConnectionStringWithPool(3, 10, 300, 10);
        }

        private string GetConnectionStringWithPool(int minPoolSize, int maxPoolSize, int idleLifeTime, int maxAutoPrepare)
        {
            var connString = new ConnectionString(Host, Port, DbName, User, Password);
            return $"{connString}MaxPoolSize={maxPoolSize};MinPoolSize={minPoolSize};Connection Idle Lifetime={idleLifeTime};Max Auto Prepare={maxAutoPrepare}";
        }
    }
}