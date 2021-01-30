using AutoMapper;
using FluentValidation;
using SuperChat.API.Controllers.Base;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.API.Controllers
{
    public class ChatRoomsController : BaseApiController<ChatRoom, ChatRoomDto>
    {
        public ChatRoomsController(IMapper mapper, 
                                IUnitOfWork<SuperChatDbContext> uow, 
                                IValidatorFactory validationFactory) 
            : base(mapper, uow, validationFactory)
        {
        }
    }
}
