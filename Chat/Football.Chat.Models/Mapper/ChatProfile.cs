using AutoMapper;
using Football.Chat.Models.Dal;
using Football.Chat.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Chat.Models.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            this.CreateMap<MessageCreateDto, Message>();
            this.CreateMap<Message, MessageDto>()
                .ForMember(dto => dto.Alias, conf => conf.MapFrom(e => e.PlayerId));
        }
    }
}
