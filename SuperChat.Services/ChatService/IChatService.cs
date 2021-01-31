using SuperChat.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatService
{
    public interface IChatService
    {
        Task<List<ChatRoomMessageDto>> GetChatHistory(int chatRoomId, int top = 50);
        Task<ChatRoomMessageDto> ProcessHubMessage(HubMessageDto hubMessage);
    }
}
