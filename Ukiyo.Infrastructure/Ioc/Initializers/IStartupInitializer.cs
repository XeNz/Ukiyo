namespace Ukiyo.Infrastructure.Ioc
{
    public interface IStartupInitializer : IInitializer
    {
        void AddInitializer(IInitializer initializer);
    }
}