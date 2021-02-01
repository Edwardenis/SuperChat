using AutoMapper;
using SuperChat.BL.DTOs.Base;
using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Repositories;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.Base
{
    public class BaseEntityService<TEntity, TEntityDto> : IBaseEntityService<TEntity, TEntityDto>
        where TEntity : class, IBaseEntity
         where TEntityDto : class, IBaseEntityDto
    {
        protected readonly IUnitOfWork<SuperChatDbContext> _uow;
        protected readonly IMapper _mapper;
        public BaseEntityService(IUnitOfWork<SuperChatDbContext> uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<TEntityDto> Create(TEntityDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);

            _uow.GetRepository<TEntity>().Add(entity);
            await _uow.Commit();

            entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        public async Task<TEntityDto> Delete(int id)
        {
            TEntity entity = await GetById(id);

            if (entity is null)
                return null;

            _uow.GetRepository<TEntity>().Delete(entity);
            await _uow.Commit();

            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            return entityDto;
        }

        public Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate = null,
                int? page = null,
                int? pageSize = null,
                SortExpression<TEntity> sortExpressions = null)
        {
            return _uow.GetRepository<TEntity>().Get(predicate,
                page,
                pageSize,
                sortExpressions);
        }

        public Task<TEntity> GetById(int id)
        {
            return _uow.GetRepository<TEntity>().GetById(id);
        }

        public TEntityDto Map(TEntity entity)
        {
            return _mapper.Map<TEntityDto>(entity);
        }

        public IEnumerable<TEntityDto> Map(IEnumerable<TEntity> entity)
        {
            return _mapper.Map<IEnumerable<TEntityDto>>(entity);
        }

        public async Task<TEntityDto> Update(int id, TEntityDto entityDto)
        {
            TEntity entity = await GetById(id);
            if (entity is null)
                return null;

            _mapper.Map(entityDto, entity);

            _uow.GetRepository<TEntity>().Update(entity);

            await _uow.Commit();

            return _mapper.Map(entity, entityDto);
        }
    }
}
