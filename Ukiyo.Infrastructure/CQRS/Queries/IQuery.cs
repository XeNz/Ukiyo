namespace Ukiyo.Infrastructure.CQRS.Queries
{
    public interface IQuery
    {
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IQuery<T> : IQuery
    {
    }
}