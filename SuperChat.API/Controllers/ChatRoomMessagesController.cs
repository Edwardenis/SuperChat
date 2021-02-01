using FluentValidation;
using SuperChat.API.Controllers.Base;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using SuperChat.Services.ChatRoomMessageService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.API.Controllers
{
    public class ChatRoomMessagesController : BaseApiController<ChatRoomMessage, ChatRoomMessageDto>
    {
        public ChatRoomMessagesController(IChatRoomMessageService service,
                                IValidatorFactory validationFactory)
            : base(service, validationFactory)
        {
        }
    }
}
