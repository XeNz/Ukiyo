namespace Ukiyo.Infrastructure.Ioc.Initializers
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}