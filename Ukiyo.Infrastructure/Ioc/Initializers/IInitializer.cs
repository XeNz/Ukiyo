using System.Threading.Tasks;

namespace Ukiyo.Infrastructure.Ioc
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}