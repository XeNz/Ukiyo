namespace Ukiyo.Infrastructure.CQRS.Queries
{
    public interface IQuery
    {
    }

    public interface IQuery<T> : IQuery
    {
    }
}