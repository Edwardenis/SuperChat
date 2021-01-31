using MassTransit;
using Microsoft.Extensions.Logging;
using SuperChat.BL.DTOs;
using SuperChat.BL.QueueModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Bot.Consumers
{
    public class StockRequestConsumer : IConsumer<StockRequest>
    {
        private readonly ILogger _logger;
        public StockRequestConsumer(ILogger<StockRequestConsumer> logger)
        {
            _logger = logger;
        }
        public Task Consume(ConsumeContext<StockRequest> context)
        {
            var stockCode = context.Message.StockCode;

            var stockResponse = new StockResponse
            {
                ConnectionId = context.Message.ConnectionId,
                Stock = new StockDto
                {
                    Close = "125.362",
                    Symbol = stockCode
                }
            };
            context.Publish<StockResponse>(stockResponse);
            _logger.LogInformation("Stock request recieved {0}", stockCode);
            return Task.CompletedTask;
        }
    }
}
