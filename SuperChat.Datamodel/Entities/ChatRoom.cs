using SuperChat.Core.Basemodel.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel.Entities
{
    public class ChatRoom : BaseEntity
    {
        public ChatRoom()
        {
            this.Messages = new HashSet<ChatRoomMessage>();
        }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<ChatRoomMessage> Messages { get; set; }
    }
}
