using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Contexts;
using SuperChat.Datamodel.Entities;
using SuperChat.Datamodel.Repositories;
using SuperChat.Datamodel.UnitOfWork;
using SuperChat.Services.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SuperChat.Services.ChatRoomMessageService
{
    public class ChatRoomMessageService : BaseEntityService<ChatRoomMessage, ChatRoomMessageDto>, IChatRoomMessageService
    {
        public ChatRoomMessageService(IUnitOfWork<SuperChatDbContext> uow,
            IMapper mapper)
            :base(uow, mapper)
        {
        }

        public async Task<IEnumerable<ChatRoomMessageDto>> GetChatMessagesByChatRoom(int chatRoomId, 
                                    int top = 50,
                                    bool ascending = true)
        {
            var sort = ascending
                ? ListSortDirection.Ascending
                : ListSortDirection.Descending;

            var entities = await Get(c => c.ChatRoomId == chatRoomId,
                        page: 1,
                        pageSize: top,
                        sortExpressions: new SortExpression<ChatRoomMessage>(p => p.OcurredAt, sort));

            
            var dtos = _mapper.Map<List<ChatRoomMessageDto>>(entities);
            return dtos;
        }
    }
}
