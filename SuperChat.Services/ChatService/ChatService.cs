using SuperChat.BL.DTOs;
using SuperChat.Services.ChatRoomMessageService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRoomMessageService _chatRoomMessageService;
        public ChatService(IChatRoomMessageService chatRoomMessageService)
        {
            _chatRoomMessageService = chatRoomMessageService;
        }
        public async Task<List<ChatRoomMessageDto>> GetChatHistory(int chatRoomId, int top = 50)
        {
            var messages = await _chatRoomMessageService
                .GetChatMessagesByChatRoom(chatRoomId, 
                                    top: 50, 
                                    ascending: false);

            return messages;
        }

        public Task<ChatRoomMessageDto> ProcessHubMessage(HubMessageDto hubMessage)
        {
            return Task.FromResult(new ChatRoomMessageDto
            {
                OcurredAt = hubMessage.OcurredAt,
                MessageText = hubMessage.MessageText,
                ChatRoomCode = hubMessage.ChatRoomCode,
                FromUser = hubMessage.FromUser
            });
        }
    }
}
