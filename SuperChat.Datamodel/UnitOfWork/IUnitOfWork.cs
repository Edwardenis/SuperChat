using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Datamodel.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity;
        Task<int> Commit();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork
    {
        TContext Context { get; }
    }
}
