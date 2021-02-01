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
        private readonly ILogger<StockRequestConsumer> _logger; 
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
            var stockResponse = new StockResponse
            {
                Success = true,
                ConnectionId = context.Message.ConnectionId,
                StockCode = stockCode,
            };
            StockDto stock = null;
            try
            {
                stock = await _stockService.GetStock(stockCode);
            } 
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                stockResponse.Success = false;
            }
            stockResponse.Stock = stock;
            //
            await context.Publish(stockResponse);
            _logger.LogInformation("Stock request recieved {0}", stockCode);
        }
    }
}
