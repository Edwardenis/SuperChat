using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.UnitOfWork;
using SuperChat.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomMessageService
{
    public class ChatRoomMessageService : BaseEntityService, IChatRoomMessageService
    {
        public ChatRoomMessageService(IUnitOfWork<SuperChatDbContext> uow,
            IMapper mapper)
            :base(uow, mapper)
        {
        }

        public async Task<ChatRoomMessageDto> CreateMessage(ChatRoomMessageDto chatRoomMessageDto)
        {
            var entity = _mapper.Map<ChatRoomMessage>(chatRoomMessageDto);
            _uow
                .GetRepository<ChatRoomMessage>()
                .Add(entity);
            //
            await _uow.Commit();

            chatRoomMessageDto.Id = entity.Id;

            return chatRoomMessageDto;
        }

        public async Task<IEnumerable<ChatRoomMessageDto>> GetChatMessagesByChatRoom(int chatRoomId, 
                                    int top = 50,
                                    bool ascending = true)
        {
            var query = _uow.GetRepository<ChatRoomMessage>()
                .Get(c => c.ChatRoomId == chatRoomId);

            
            if (ascending)
                query = query.OrderBy(x => x.OcurredAt);
            else
                query = query.OrderByDescending(x => x.OcurredAt);

            var entities = await query
                .Take(top)
                .ToListAsync();
            var dtos = _mapper.Map<List<ChatRoomMessageDto>>(entities);
            return dtos;
        }
    }
}
