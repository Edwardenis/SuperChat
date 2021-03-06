﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.UnitOfWork;
using SuperChat.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomService
{
    public class ChatRoomService : BaseEntityService<ChatRoom, ChatRoomDto>, IChatRoomService
    {
        public ChatRoomService(IUnitOfWork<SuperChatDbContext> uow,
            IMapper mapper)
            :base(uow, mapper)
        {
        }
        public async Task<IEnumerable<ChatRoomDto>> GetChatRooms(int top = 10)
        {
            var allChatRooms = await Get(page: 1, pageSize: top);
            var chatRoomsDto = _mapper.Map<List<ChatRoomDto>>(allChatRooms);

            return chatRoomsDto;
        }
    }
}
