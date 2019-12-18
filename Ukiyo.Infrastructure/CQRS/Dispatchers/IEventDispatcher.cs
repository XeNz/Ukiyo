using System.Threading.Tasks;
using Ukiyo.Infrastructure.CQRS.Events;

namespace Ukiyo.Infrastructure.CQRS.Dispatchers
{
    public interface IEventDispatcher
    {
        Task PublishAsync<T>(T @event) where T : class, IEvent;
    }
}