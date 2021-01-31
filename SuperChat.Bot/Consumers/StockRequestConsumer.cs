using MassTransit;
using Microsoft.Extensions.Logging;
using SuperChat.BL.DTOs;
using SuperChat.BL.QueueModels;
using SuperChat.Services.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Bot.Consumers
{
    public class StockRequestConsumer : IConsumer<StockRequest>
    {
        private readonly ILogger _logger; 
        private readonly IStockService _stockService; 
        public StockRequestConsumer(ILogger<StockRequestConsumer> logger,
                                IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }
        public async Task Consume(ConsumeContext<StockRequest> context)
        {
            var stockCode = context.Message.StockCode;
            var stock = await _stockService.GetStock(stockCode);
            //
            var stockResponse = new StockResponse
            {
                ConnectionId = context.Message.ConnectionId,
                StockCode = stockCode,
                Stock = stock
            };
            await context.Publish(stockResponse);
            _logger.LogInformation("Stock request recieved {0}", stockCode);
        }
    }
}
