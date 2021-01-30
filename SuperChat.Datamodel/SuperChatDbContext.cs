using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel
{
    public class SuperChatDbContext : IdentityDbContext<AppUser>
    {
        public SuperChatDbContext(DbContextOptions<SuperChatDbContext> options)
            : base(options)
        {
        }
    }
}
