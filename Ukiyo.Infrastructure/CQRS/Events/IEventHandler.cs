using System.Threading.Tasks;

namespace Ukiyo.Infrastructure.CQRS.Events
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}