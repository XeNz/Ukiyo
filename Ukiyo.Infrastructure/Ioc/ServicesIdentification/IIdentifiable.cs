namespace Ukiyo.Infrastructure.Ioc.ServicesIdentification
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}