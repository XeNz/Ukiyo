using System.Threading.Tasks;
using Ukiyo.Infrastructure.CQRS.Commands;

namespace Ukiyo.Infrastructure.CQRS.Dispatchers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}