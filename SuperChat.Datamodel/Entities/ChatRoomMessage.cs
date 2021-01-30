using SuperChat.Core.Basemodel.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel.Entities
{
    public class ChatRoomMessage : BaseEntity
    {
        public string MessageText { get; set; }

        public DateTimeOffset OcurredAt { get; set; }

        public string FromUser { get; set; }

        public int ChatRoomId { get; set; }

        public virtual ChatRoom ChatRoom { get; set; }

    }
}
