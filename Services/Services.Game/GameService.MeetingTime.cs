using Football.Exceptions;
using Models.Data;
using Models.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Game
{
    public partial class GameService
    {
        public async Task AddMeetingTimeAsync(MeetingTime meetingTime)
        {
            var game = await this.gameRepository.GetAsync(meetingTime.GameId) ?? throw new NotFoundException($"Game with id:{meetingTime.GameId} not exists.");

            if (game.MeetingTimes.Any(mt => mt.TimeOfMeet == meetingTime.TimeOfMeet))
            {
                return;
            }

            game.MeetingTimes.Add(meetingTime);
            await gameRepository.UpdateAsync(game);
        }

        public async Task AddMeetingTimeAsync(DateTimeOffset meetingtime, Guid gameId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id:{gameId} not exists.");

            if (game.MeetingTimes.Any(mt => mt.TimeOfMeet == meetingtime))
            {
                throw new DublicateException($"Game has meeting time variant {meetingtime}");
            }

            game.MeetingTimes.Add(new MeetingTime { Id = Guid.NewGuid(), GameId = gameId, TimeOfMeet = meetingtime });
            await gameRepository.UpdateAsync(game);
        }

        public async Task SetChosenTimeAsync(Guid gameId, Guid meetingtimeId, string playerId)
        {
            var game = await this.gameRepository.GetAsync(gameId) ?? throw new NotFoundException($"Game with id {gameId} not exists.");

            if (game.AdminId != playerId)
            {
                throw new ForbiddenException($"Player with playerId {playerId} is not admin in game.");
            }

            var meetingtime = game.MeetingTimes.FirstOrDefault(mt => mt.Id == meetingtimeId) ?? throw new NotFoundException($"MeetingTime with id {meetingtimeId} not exists.");

            var oldMeetingTime = game.MeetingTimes.FirstOrDefault(mt => mt.IsChosen);
            if (oldMeetingTime != null)
            {
                oldMeetingTime.IsChosen = false;
            }

            meetingtime.IsChosen = true;

            await this.gameRepository.UpdateAsync(game);

            var batchMessage = new BatchMessage
            {
                Title = $"Time is chosen for {game.Name}",
                Text = $"{game.AdminId} sets time - {meetingtime.TimeOfMeet}",
                UsersIds = game.PlayerGames.Select(pg => pg.PlayerId).Where(id => id != game.AdminId).ToList()
            };

            await this.pushNotificationService.Notify(batchMessage);
        }
    }
}
