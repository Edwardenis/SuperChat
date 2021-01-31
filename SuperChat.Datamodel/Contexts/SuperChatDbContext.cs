using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Datamodel.Contexts
{
    public class SuperChatDbContext : BaseDbContext
    {
        public SuperChatDbContext(DbContextOptions<SuperChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //
            builder.Entity<ChatRoom>()
                .HasIndex(u => u.Code)
                .IsUnique();

            builder.Entity<ChatRoom>()
                .HasData(
                    new ChatRoom { 
                        Id = 1, 
                        Name = "Default Chat room", 
                        Code = "Test1",
                        CreatedAt = DateTimeOffset.UtcNow
                    },
                    new ChatRoom
                    {
                        Id = 2,
                        Name = "Default Chat room 2",
                        Code = "Test2",
                        CreatedAt = DateTimeOffset.UtcNow
                    }
                );
        }
    }
}
