using AutoMapper;
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
        private readonly IMapper _mapper;
        public ChatService(IChatRoomMessageService chatRoomMessageService,
                        IStockRequesterService stockRequesterService,
                        IMapper mapper)
        {
            _chatRoomMessageService = chatRoomMessageService;
            _stockRequesterService = stockRequesterService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ChatRoomMessageDto>> GetChatHistory(int chatRoomId, int top = 50)
        {
            var messages = await _chatRoomMessageService
                .GetChatMessagesByChatRoom(chatRoomId, 
                                    top: top, 
                                    ascending: false);

            return messages;
        }

        public async Task<IEnumerable<ChatRoomMessageDto>> ProcessHubMessage(HubMessageDto hubMessage)
        {
            var chatRoomMessageDto = _mapper.Map<ChatRoomMessageDto>(hubMessage);
            var responseMessages = new List<ChatRoomMessageDto>();
            responseMessages.Add(chatRoomMessageDto);
            //
            if (hubMessage.IsCommandMessage)
            {
                
                switch (hubMessage.CommandName)
                {
                    case SupportedBotCommands.STOCK:
                        if (string.IsNullOrEmpty(hubMessage.CommandParameter))
                            goto default;

                        var request = new StockRequest
                        {
                            StockCode = hubMessage.CommandParameter,
                            RequestedBy = hubMessage.FromUser,
                            ConnectionId = hubMessage.ConnectionId
                        };
                        await _stockRequesterService.RequestStock(request);
                        break;
                    default:
                        var message = string.IsNullOrEmpty(hubMessage.CommandParameter)
                            ? "Command parameter cannot be empty."
                            : "Sorry but I don't know that command. Try something like /stock={symbol}";
                        responseMessages
                            .Add(new ChatRoomMessageDto
                            {
                                ChatRoomCode = hubMessage.ChatRoomCode,
                                ChatRoomId = hubMessage.ChatRoomId,
                                FromUser = "Bot",
                                OcurredAt = DateTimeOffset.UtcNow,
                                MessageText = message
                            });
                        break;
                }

                return responseMessages;
            }
            
            await _chatRoomMessageService.Create(chatRoomMessageDto);

            return responseMessages;
        }
    }
}
