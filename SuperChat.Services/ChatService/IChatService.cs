using SuperChat.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatService
{
    public interface IChatService
    {
        Task<IEnumerable<ChatRoomMessageDto>> GetChatHistory(int chatRoomId, int top = 50);
        Task<IEnumerable<ChatRoomMessageDto>> ProcessHubMessage(HubMessageDto hubMessage);
    }
}
