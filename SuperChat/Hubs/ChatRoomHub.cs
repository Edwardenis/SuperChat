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

            await Clients.Client(Context.ConnectionId).SendAsync(EventsConstants.CHAT_HISTORY, messages);
        }
        public async Task SendMessage(string chatRoomCode, string chatRoomId, string user, string message)
        {
            var hubMessage = new HubMessageDto
            {
                ConnectionId = Context.ConnectionId,
                ChatRoomId = int.Parse(chatRoomId),
                ChatRoomCode = chatRoomCode,
                FromUser = user,
                MessageText = message,
                OcurredAt = DateTimeOffset.UtcNow
            };
            var createdMessageEntity = await _chatService.ProcessHubMessage(hubMessage);
            //
            if (hubMessage.IsCommandMessage)
            {
                //If it was a command message, only print message for sender
                await Clients.Client(Context.ConnectionId).SendAsync(EventsConstants.RECEIVE_MESSAGE, createdMessageEntity);
            }
            else
            {
                //Send message to group
                await Clients.Group(chatRoomCode).SendAsync(EventsConstants.RECEIVE_MESSAGE, createdMessageEntity);
            }
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
