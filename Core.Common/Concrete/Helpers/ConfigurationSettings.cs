namespace Core.Authorization.Common.Concrete.Helpers
{
    /// <summary>
    /// Helper for get configuration
    /// </summary>
    /// Initial author Sergey Sushenko
    public class ConfigurationSettings
    {
        public string ConnectionString { get; set; }
        public AppSettings AppSettings { get; set; }
    }

    public class AppSettings
    {
        public string JwtPublicKey { get; set; }
        public string RedisServer { get; set; }
        public int RedisPort { get; set; }
    }
}
