using MassTransit;
using SuperChat.BL.QueueModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.RabbitMQ
{
    public class StockRequesterService : IStockRequesterService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public StockRequesterService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async Task RequestStock(StockRequest stockRequest)
        {
            await _publishEndpoint.Publish(stockRequest);
        }
    }
}
