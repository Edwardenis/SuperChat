using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using SuperChat.BL.DTOs;
using SuperChat.BL.QueueModels;
using SuperChat.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Consumers
{
    public class StockResponseConsumer : IConsumer<StockResponse>
    {
        private readonly ILogger _logger;
        private readonly IHubContext<ChatRoomHub> _hubContext;
        public StockResponseConsumer(ILogger<StockResponseConsumer> logger,
                            IHubContext<ChatRoomHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public async Task Consume(ConsumeContext<StockResponse> context)
        {
            var connectionId = context.Message.ConnectionId;
            var message = new ChatRoomMessageDto
            {
                FromUser = "Bot",
                OcurredAt = DateTimeOffset.UtcNow,
            };
            //
            if (context.Message.Stock == null)
            {
                //If stock = null, means could not find the stock with that stock code
                message.MessageText = $"Sorry but could not found stock with code \"{context.Message.StockCode}\"";
            }
            else
            {
                var stockCode = context.Message.Stock.Symbol.ToUpper();
                var closeValue = string.Format("${0:N2}%", context.Message.Stock.Close);
                message.MessageText = $"{stockCode} quote is {closeValue} per share";
                _logger.LogInformation("Stock request recieved {0}", stockCode);
            }
            
            await _hubContext.Clients.Client(connectionId).SendAsync(EventsConstants.RECEIVE_MESSAGE, message);
            //
            
        }
    }
}
