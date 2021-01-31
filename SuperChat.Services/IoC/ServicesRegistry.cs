using Microsoft.Extensions.DependencyInjection;
using SuperChat.Services.ChatRoomMessageService;
using SuperChat.Services.ChatRoomService;
using SuperChat.Services.ChatService;
using SuperChat.Services.JWTFactory;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.Services.IoC
{
    public static class ServicesRegistry
    {
        public static void AddServicesRegistry(this IServiceCollection services)
        {
            services.AddScoped<IJwtFactory, JwtFactory>(); 
            services.AddScoped<IChatRoomService, ChatRoomService.ChatRoomService>(); 
            services.AddScoped<IChatRoomMessageService, ChatRoomMessageService.ChatRoomMessageService>();
            services.AddScoped<IChatService, ChatService.ChatService>(); 
        }
    }
}
