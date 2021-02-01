using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using SuperChat.Services.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomService
{
    public interface IChatRoomService : IBaseEntityService<ChatRoom, ChatRoomDto>
    {
        Task<IEnumerable<ChatRoomDto>> GetChatRooms(int top = 10);
    }
}
