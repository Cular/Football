using AutoMapper;
using Football.Core;
using Football.Core.Extensions;
using Models.Data;
using Models.Data.GameState;
using Models.Dto;
using System;

namespace Models.Mapper
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            this.CreateMap<Player, PlayerDto>()
                .ForMember(dto => dto.Alias, conf => conf.MapFrom(e => e.Id));

            this.CreateMap<PlayerDtoCreate, Player>()
                .ForMember(e => e.Id, conf => conf.MapFrom(dto => dto.Alias))
                .ForMember(e => e.PasswordHash, conf => conf.MapFrom(dto => PasswordHasher.GetPasswordHash(dto.Password)));

            this.CreateMap<Game, GameDto>()
                .ForMember(dto => dto.GameState, conf => conf.MapFrom(e => e.State.ToEnum()));

            this.CreateMap<GameCreateDto, Game>()
                .ForMember(e => e.State, conf => conf.MapFrom(dto => GameStateEnum.Public.ToState()));
        }
    }
}
