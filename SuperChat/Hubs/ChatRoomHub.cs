using Microsoft.AspNetCore.SignalR;
using SuperChat.BL.DTOs;
using SuperChat.Services.ChatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperChat.Hubs
{
    public class ChatRoomHub : Hub
    {
        private readonly IChatService _chatService;
        public ChatRoomHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task RequestChatHistory(string chatRoomId)
        {
            var messages = await _chatService.GetChatHistory(int.Parse(chatRoomId));

            await Clients.Client(Context.ConnectionId).SendAsync("ChatHistory", messages);
        }
        public async Task SendMessage(string chatRoomCode, string user, string message)
        {
            var hubMessage = new HubMessageDto
            {
                ChatRoomCode = chatRoomCode,
                FromUser = user,
                MessageText = message,
                OcurredAt = DateTimeOffset.UtcNow
            };
            var createdMessageEntity = await _chatService.ProcessHubMessage(hubMessage);
            //If this is null, means that won't display this message on chat history.
            if (createdMessageEntity != null)
                await Clients.Group(chatRoomCode).SendAsync("ReceiveMessage", createdMessageEntity);
        }
        public async Task AddToChatRoom(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromChatRoom(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
