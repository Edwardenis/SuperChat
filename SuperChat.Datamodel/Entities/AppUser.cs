using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
