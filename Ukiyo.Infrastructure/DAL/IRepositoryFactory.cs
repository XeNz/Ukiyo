using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Infrastructure.DAL
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity, IAggregateRoot;
    }
}