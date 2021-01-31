using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.DTOs
{
    public class HubMessageDto
    {
        public string MessageText { get; set; }

        public DateTimeOffset OcurredAt { get; set; }

        public string FromUser { get; set; }

        public string ChatRoomCode { get; set; }
    }
}
