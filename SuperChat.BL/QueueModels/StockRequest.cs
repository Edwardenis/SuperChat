using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.QueueModels
{
    public class StockRequest
    {
        public string RequestedBy { get; set; }
        public string ConnectionId { get; set; }
        public string StockCode { get; set; }
    }
}
