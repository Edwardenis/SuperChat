using CsvHelper;
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
    public class StockService : IStockService, IDisposable
    {
        private readonly StockServiceSettings _stockServiceSettings;
        private readonly HttpClient _httpClient;
        private bool disposed;
        public StockService(IHttpClientFactory httpClientFactory, StockServiceSettings stockServiceSettings)
        {
            _stockServiceSettings = stockServiceSettings;
            _httpClient = httpClientFactory.CreateClient();
        }

        //Finalizer to dispose undisposed objects
        ~StockService()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                //Dispose managed objects
                _httpClient.Dispose();
            }
            disposed = true;
        }

        public async Task<StockDto> GetStock(string stockCode)
        {
            var url = string.Format(_stockServiceSettings.Url, stockCode);
            var httpResponse = await _httpClient.GetAsync(url);
            var text = await httpResponse.Content.ReadAsStringAsync();
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
    }
}
