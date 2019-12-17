using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Infrastructure.DAL
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork : IDisposable
        {
            IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, IAggregateRoot;

            Task<int> SaveChangesAsync();
        }

        public interface IUnitOfWork<out TContext> : IUnitOfWork where TContext : DbContext
        {
            TContext Context { get; }
        }
    }
}