using Moq;
using NUnit.Framework;
using SuperChat.Core.ConfigModels;
using SuperChat.Services.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Test
{
    [TestFixture]
    public class StockService_GetStock
    {
        private IStockService _stockService;
        [SetUp]
        public void Setup()
        {
            _stockService = new StockService(new StockServiceSettings
            {
                Url = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv"
            });
        }

        [TestCase("aapl.us")]
        public async Task GetStock_ValidStockCode_ReturnTrue(string value)
        {
            var stock = await _stockService.GetStock(value);
            Assert.IsTrue(stock != null, $"{value} should be a valid stock symbol");
        }

        [TestCase("gg89")]
        public async Task GetStock_InvalidStockCode_ReturnFalse(string value)
        {
            var stock = await _stockService.GetStock(value);
            Assert.IsFalse(stock != null, $"{value} should not be a valid stock symbol");
        }
    }
}
