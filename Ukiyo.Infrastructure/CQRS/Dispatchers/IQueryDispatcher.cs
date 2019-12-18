using System.Threading.Tasks;
using Ukiyo.Infrastructure.CQRS.Queries;

namespace Ukiyo.Infrastructure.CQRS.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
    }
}