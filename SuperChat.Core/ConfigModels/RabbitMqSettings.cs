using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Core.ConfigModels
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public string StockRequestQueueName { get; set; }
    }
}
