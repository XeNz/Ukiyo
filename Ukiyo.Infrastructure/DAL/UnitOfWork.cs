using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ukiyo.Infrastructure.DAL.Base;

namespace Ukiyo.Infrastructure.DAL
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork.IUnitOfWork<TContext>, IUnitOfWork
        where TContext : DbContext, IDisposable
    {
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, IAggregateRoot
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new EfRepository<TEntity>(Context);
            return (IRepository<TEntity>) _repositories[type];
        }

        public TContext Context { get; }


        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}