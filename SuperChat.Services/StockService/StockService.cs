using CsvHelper;
using Microsoft.Extensions.Logging;
using SuperChat.BL.DTOs;
using SuperChat.Core.ConfigModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly StockServiceSettings _stockServiceSettings;
        private readonly IHttpClientFactory _httpClientFactory; 
        private readonly ILogger<StockService> _logger;
        public StockService(IHttpClientFactory httpClientFactory, 
            StockServiceSettings stockServiceSettings,
            ILogger<StockService> logger)
        {
            _stockServiceSettings = stockServiceSettings;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<StockDto> GetStock(string stockCode)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var url = string.Format(_stockServiceSettings.Url, stockCode);
            var httpResponse = await httpClient.GetAsync(url);
            var text = await httpResponse.Content.ReadAsStringAsync();

            try
            {
                using TextReader reader = new StringReader(text);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

                var stock = csv.GetRecords<StockDto>()
                            .FirstOrDefault();
                //
                //IF Close is N/D means the stockCode was not found. Return null
                if (stock.Close == "N/D")
                    return null;

                return stock;

            }
            catch (HeaderValidationException ex)
            {
                _logger.LogError(ex, "Stock web service did not responded propertly");
                //Service did not respoded expected value
                return null;
            }
            
        }
    }
}
