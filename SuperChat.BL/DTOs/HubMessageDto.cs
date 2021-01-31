using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SuperChat.BL.DTOs
{
    public class HubMessageDto
    {
        public string MessageText { get; set; }

        public DateTimeOffset OcurredAt { get; set; }

        public string FromUser { get; set; }

        public string ChatRoomCode { get; set; }
        public int ChatRoomId { get; set; }
        public string ConnectionId { get; set; }

        public bool IsCommandMessage { get { return _commandRegex.IsMatch(MessageText);  } }
        public string CommandName { get {
                if (!IsCommandMessage)
                    return string.Empty;

                return _commandRegex.Match(MessageText).Value.ToLower();
            } 
        }

        public string CommandParameter
        {
            get
            {
                if (!IsCommandMessage)
                    return string.Empty;

                return _commandParameterRegex.Match(MessageText).Value.ToLower();
            }
        }

        private Regex _commandRegex = new Regex(@"(?<=\/)(?:(?!=).)*");
        private Regex _commandParameterRegex = new Regex(@"(?<=\=)(.*?)$");
    }
}
