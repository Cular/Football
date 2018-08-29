using AutoMapper;
using Football.Core;
using Football.Core.Extensions;
using Models.Data;
using Models.Dto;
using System;

namespace Models.Mapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            this.CreateMap<Player, PlayerDto>()
                .ForMember(dto => dto.Alias, conf => conf.ResolveUsing(e => e.Id));

            this.CreateMap<PlayerDtoCreate, Player>()
                .ForMember(e => e.Id, conf => conf.ResolveUsing(dto => dto.Alias))
                .ForMember(e => e.PasswordHash, conf => conf.ResolveUsing(dto => PasswordHasher.GetPasswordHash(dto.Password)));
        }
    }
}
