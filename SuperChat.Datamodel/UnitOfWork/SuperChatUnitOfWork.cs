using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Datamodel.UnitOfWork
{
    public class SuperChatUnitOfWork : IUnitOfWork<SuperChatDbContext>
    {
        public SuperChatDbContext Context { get; set; }
        public readonly IServiceProvider _serviceProvider;

        public SuperChatUnitOfWork(IServiceProvider serviceProvider, SuperChatDbContext context)
        {
            _serviceProvider = serviceProvider;
            this.Context = context;
        }

        public async Task<int> Commit()
        {
            var result = await Context.SaveChangesAsync();
            return result;
        }


        public void Dispose()
        {
            Context.Dispose();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
        {
            return (_serviceProvider.GetService(typeof(TEntity)) ?? new BaseRepository<TEntity>(this)) as IRepository<TEntity>;
        }
    }
}
