namespace Ukiyo.Infrastructure.Ioc
{
    public class AppOptions
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool DisplayBanner { get; set; } = true;
    }
}