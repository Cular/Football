using AutoMapper;
using Football.Core;
using Football.Core.Extensions;
using Models.Data;
using Models.Data.GameState;
using Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .ForMember(dto => dto.GameState, conf => conf.MapFrom(e => e.State.ToEnum()))
                .ForMember(dto => dto.Players, conf => conf.MapFrom(e => e.PlayerGames.Select(pg => pg.Player)));
                //.ForMember(dto => dto.MeetingTimes, conf => conf.MapFrom(e => e.MeetingTimes));

            this.CreateMap<GameCreateDto, Game>()
                .ForMember(e => e.State, conf => conf.MapFrom(dto => GameStateEnum.Public.ToState()))
                .ForMember(e => e.Id, conf => conf.MapFrom(dto => Guid.NewGuid()))
                .ForMember(e => e.PlayerGames, conf => conf.MapFrom(dto => new List<PlayerGame>()));

            this.CreateMap<MeetingTimeCreateDto, MeetingTime>();

            this.CreateMap<MeetingTime, MeetingTimeDto>()
                .ForMember(dto => dto.Players, conf => conf.MapFrom(e => e.PlayerVotes.Select(pv => pv.PlayerId)));
        }
    }
}
