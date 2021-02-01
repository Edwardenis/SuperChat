using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SuperChat.BL.DTOs.Base;
using SuperChat.Core.Basemodel.BaseEntity;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Repositories;
using SuperChat.Datamodel.UnitOfWork;
using SuperChat.Services.Base;
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
        public IValidatorFactory _validationFactory { get; set; }
        protected readonly IBaseEntityService<TEntity, TEntityDto> _baseEntityService;

        public BaseApiController(IBaseEntityService<TEntity, TEntityDto> baseEntityService,
                IValidatorFactory validationFactory)
        {
            _baseEntityService = baseEntityService;
            _validationFactory = validationFactory;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>A list of records.</returns>
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var list = await _baseEntityService.Get();
            return Ok(_baseEntityService.Map(list));
        }


        /// <summary>
        /// Get a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A specific record.</returns>
        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            TEntity entity = await _baseEntityService.GetById(id);

            if (entity is null)
                return NotFound();

            return Ok(_baseEntityService.Map(entity));
        }

        /// <summary>
        /// Creates a record.
        /// </summary>
        /// <returns>A newly created record.</returns>
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntityDto entityDto)
        {
            await _baseEntityService.Create(entityDto);

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

            var dto = await _baseEntityService.Update(id, entityDto);
            if (dto is null)
                return NotFound();

            return Ok(dto);
        }


        /// <summary>
        /// Deletes a specific record by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A deleted record.</returns>
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var deleteEntity = await _baseEntityService.Delete(id);

            if (deleteEntity is null)
                return NotFound();

            return Ok(deleteEntity);
        }

    }
}
