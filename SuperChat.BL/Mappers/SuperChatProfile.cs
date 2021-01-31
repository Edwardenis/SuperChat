using AutoMapper;
using SuperChat.BL.DTOs;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperChat.BL.Mappers
{
    public class SuperChatProfile : Profile
    {
        public SuperChatProfile()
        {
            CreateMap<ChatRoom, ChatRoomDto>()
                .ReverseMap();

            CreateMap<ChatRoomMessage, ChatRoomMessageDto>()
                .ForMember(dto => dto.ChatRoomCode,
                                cfg => cfg.MapFrom(e => e.ChatRoom.Code))
                .ReverseMap();
        }
    }
}
