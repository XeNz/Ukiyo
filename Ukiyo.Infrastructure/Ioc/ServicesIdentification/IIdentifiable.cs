namespace Ukiyo.Infrastructure.Ioc
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}