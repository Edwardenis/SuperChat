using SuperChat.BL.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.DTOs
{
    public class ChatRoomDto : BaseEntityDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
