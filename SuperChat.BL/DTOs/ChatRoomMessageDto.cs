using SuperChat.BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.DTOs
{
    public class ChatRoomMessageDto : BaseEntityDto
    {
        public string MessageText { get; set; }

        public DateTimeOffset OcurredAt { get; set; }

        public string FromUser { get; set; }

        public int ChatRoomId { get; set; }
        public string ChatRoomCode { get; set; }
    }
}
