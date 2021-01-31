using SuperChat.BL.QueueModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.RabbitMQ
{
    public interface IStockRequesterService
    {
        Task RequestStock(StockRequest stockRequest);
    }
}
