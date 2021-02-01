using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using SuperChat.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomMessageService
{
    public interface IChatRoomMessageService : IBaseEntityService<ChatRoomMessage, ChatRoomMessageDto>
    {
        Task<IEnumerable<ChatRoomMessageDto>> GetChatMessagesByChatRoom(int chatRoomId, int top = 50, bool ascending = true);
    }
}
