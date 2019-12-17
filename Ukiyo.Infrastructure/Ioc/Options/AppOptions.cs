namespace Ukiyo.Infrastructure.Ioc.Options
{
    public class AppOptions
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public bool DisplayBanner { get; set; } = true;
    }
}