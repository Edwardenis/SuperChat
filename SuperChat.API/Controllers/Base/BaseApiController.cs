using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SuperChat.BL.DTOs.Base;
using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Repositories;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SuperChat.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController<TEntity, TEntityDto> : ControllerBase, IBaseController
         where TEntity : class, IBaseEntity
         where TEntityDto : class, IBaseEntityDto
    {
        public IMapper _mapper { get; set; }
        public IValidatorFactory _validationFactory { get; set; }

        protected readonly IUnitOfWork<SuperChatDbContext> _uow;
        protected readonly IRepository<TEntity> _repository;

        public Type TypeDto { get; set; }

        public BaseApiController(IMapper mapper, 
                IUnitOfWork<SuperChatDbContext> uow,
                IValidatorFactory validationFactory)
        {
            _mapper = mapper;
            _uow = uow;
            _validationFactory = validationFactory;
            _repository = _uow.GetRepository<TEntity>();
            TypeDto = typeof(List<TEntityDto>);
        }

        /// <summary>
        /// Get all by query options.
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var list = _repository.Get();
            return Ok(list);
        }


        /// <summary>
        /// Get a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            TEntity entity = await _repository.GetById(id);

            if (entity is null)
                return NotFound();

            TEntity result = await Task.FromResult(entity);

            TEntityDto dto = _mapper.Map<TEntityDto>(result);

            return Ok(dto);
        }

        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <returns>A newly created record.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityDto entityDto)
        {
            TEntity entity = _mapper.Map<TEntity>(entityDto);

            _repository.Add(entity);
            await _uow.Commit();

            entityDto = _mapper.Map<TEntityDto>(entity);

            return CreatedAtAction(WebRequestMethods.Http.Get, new { id = entityDto.Id }, entityDto);
        }

        /// <summary>
        /// Updates a record.
        /// </summary>
        /// <returns>No Content.</returns>
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] TEntityDto entityDto)
        {
            if (entityDto.Id != id)
                return BadRequest();

            TEntity entity = await _repository.GetById(id);
            if (entity is null)
                return NotFound();

            _mapper.Map(entityDto, entity);

            _repository.Update(entity);

            await _uow.Commit();

            return Ok(_mapper.Map(entity, entityDto));
        }


        /// <summary>
        /// Deletes a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A deleted record.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            TEntity entity = await _repository.GetById(id);

            if (entity is null)
                return NotFound();

            _repository.Delete(entity);
            await _uow.Commit();

            TEntityDto entityDto = _mapper.Map<TEntityDto>(entity);

            return Ok(entityDto);
        }

    }
}
