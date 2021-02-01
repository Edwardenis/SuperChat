using SuperChat.BL.DTOs.Base;
using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.Base
{
    public interface IBaseEntityService<TEntity, TEntityDto>
        where TEntity : class, IBaseEntity
         where TEntityDto : class, IBaseEntityDto
    {
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate = null,
                int? page = null,
                int? pageSize = null,
                SortExpression<TEntity> sortExpressions = null);

        Task<TEntity> GetById(int id);

        Task<TEntityDto> Create(TEntityDto entityDto);
        Task<TEntityDto> Update(int id, TEntityDto entityDto);
        Task<TEntityDto> Delete(int id);

        TEntityDto Map(TEntity entity);
        IEnumerable<TEntityDto> Map(IEnumerable<TEntity> entity);
    }
}
