using SuperChat.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.QueueModels
{
    public class StockResponse
    {
        public string ConnectionId { get; set; }
        public StockDto Stock { get; set; }
    }
}
