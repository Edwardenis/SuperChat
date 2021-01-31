using AutoMapper;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Services.Base
{
    public class BaseEntityService
    {
        protected readonly IUnitOfWork<SuperChatDbContext> _uow;
        protected readonly IMapper _mapper;
        public BaseEntityService(IUnitOfWork<SuperChatDbContext> uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
    }
}
