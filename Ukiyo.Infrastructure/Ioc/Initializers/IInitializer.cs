using System.Threading.Tasks;

namespace Ukiyo.Infrastructure.Ioc.Initializers
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}