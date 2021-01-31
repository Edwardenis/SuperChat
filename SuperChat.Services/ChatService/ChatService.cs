using SuperChat.BL.DTOs;
using SuperChat.BL.QueueModels;
using SuperChat.Core.Constants;
using SuperChat.Services.ChatRoomMessageService;
using SuperChat.Services.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IChatRoomMessageService _chatRoomMessageService;
        private readonly IStockRequesterService _stockRequesterService;
        public ChatService(IChatRoomMessageService chatRoomMessageService,
                        IStockRequesterService stockRequesterService)
        {
            _chatRoomMessageService = chatRoomMessageService;
            _stockRequesterService = stockRequesterService;
        }
        public async Task<List<ChatRoomMessageDto>> GetChatHistory(int chatRoomId, int top = 50)
        {
            var messages = await _chatRoomMessageService
                .GetChatMessagesByChatRoom(chatRoomId, 
                                    top: 50, 
                                    ascending: false);

            return messages;
        }

        public async Task<ChatRoomMessageDto> ProcessHubMessage(HubMessageDto hubMessage)
        {
            if (hubMessage.IsCommandMessage)
            {
                switch (hubMessage.CommandName)
                {
                    case SupportedBotCommands.STOCK:
                        var request = new StockRequest
                        {
                            StockCode = hubMessage.CommandParameter,
                            RequestedBy = hubMessage.FromUser,
                            ConnectionId = hubMessage.ConnectionId
                        };
                        await _stockRequesterService.RequestStock(request);
                        break;
                    default:

                        break;
                }
                return null;
            }

            var chatRoomMessageDto = new ChatRoomMessageDto
            {
                ChatRoomId = hubMessage.ChatRoomId,
                OcurredAt = hubMessage.OcurredAt,
                MessageText = hubMessage.MessageText,
                ChatRoomCode = hubMessage.ChatRoomCode,
                FromUser = hubMessage.FromUser
            };

            chatRoomMessageDto = await _chatRoomMessageService.CreateMessage(chatRoomMessageDto);


            return chatRoomMessageDto;
        }
    }
}
