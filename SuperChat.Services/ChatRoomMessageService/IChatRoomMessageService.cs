﻿using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomMessageService
{
    public interface IChatRoomMessageService
    {
        Task<IEnumerable<ChatRoomMessageDto>> GetChatMessagesByChatRoom(int chatRoomId, int top = 50, bool ascending = true);
        Task<ChatRoomMessageDto> CreateMessage(ChatRoomMessageDto chatRoomMessageDto);
    }
}
