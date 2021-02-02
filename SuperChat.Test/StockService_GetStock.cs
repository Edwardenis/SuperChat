using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using SuperChat.Core.ConfigModels;
using SuperChat.Services.Stock;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperChat.Test
{
    [TestFixture]
    public class StockService_GetStock
    {
        private Mock<IHttpClientFactory> _fakeHttpClientFactory;
        private StockServiceSettings _stockServiceSettings;
        private Mock<ILogger<StockService>> _fakeLogger;
        [SetUp]
        public void Setup()
        {
            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeLogger = new Mock<ILogger<StockService>>();
            _stockServiceSettings = new StockServiceSettings
            {
                Url = "https://google.com"
            };
            //
        }

        [Test]
        public async Task GetStock_ValidWebServiceResponse_ReturnTrue()
        {
            SetupDefaulHttpClientResponseValue(@"Symbol,Date,Time,Open,High,Low,Close,Volume
AAPL.US,2021-01-29,22:00:27,135.83,136.74,130.21,131.96,177523812");
            IStockService stockService = new StockService(_fakeHttpClientFactory.Object, _stockServiceSettings, _fakeLogger.Object);
            var stock = await stockService.GetStock("aapl.us");
            Assert.IsTrue(stock != null);
        }

        [Test]
        public async Task GetStock_InvalidWebServiceResponse_ReturnFalse()
        {
            SetupDefaulHttpClientResponseValue(@"Ticker missing");
            IStockService stockService = new StockService(_fakeHttpClientFactory.Object, _stockServiceSettings, _fakeLogger.Object);
            var stock = await stockService.GetStock("aapl.us");
            Assert.IsFalse(stock != null);
        }

        private void SetupDefaulHttpClientResponseValue(string response)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(response),
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            _fakeHttpClientFactory
                .Setup(_ => _.CreateClient(It.IsAny<string>()))
                .Returns(client);

        }
    }
}
