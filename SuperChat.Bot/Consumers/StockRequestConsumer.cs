using MassTransit;
using Microsoft.Extensions.Logging;
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

            _logger.LogInformation("Stock request recieved {0}", stockCode);
            return Task.CompletedTask;
        }
    }
}
