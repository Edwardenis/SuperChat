using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Models.Configs
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
