using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomService
{
    public interface IChatRoomService
    {
        Task<List<ChatRoomDto>> GetChatRooms(int top = 10);
    }
}
