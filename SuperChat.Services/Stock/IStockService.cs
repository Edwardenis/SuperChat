using SuperChat.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.Stock
{
    public interface IStockService
    {
        Task<StockDto> GetStock(string stockCode);
    }
}
