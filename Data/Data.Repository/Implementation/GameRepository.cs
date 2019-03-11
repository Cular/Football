// <copyright file="GameRepository.cs" company="Yarik Home">
// All rights maybe reserved. © Yarik
// </copyright>

namespace Data.Repository.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dapper;
    using Data.DataBaseContext;
    using Data.Repository.Interfaces;
    using Data.Repository.TypeHandlers;
    using Microsoft.EntityFrameworkCore;
    using Models.Data;
    using Models.Data.GameState;

    /// <summary>
    /// The game repository.
    /// </summary>
    public class GameRepository : BaseRepository<Game, Guid>, IGameRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The db connectionString.</param>
        public GameRepository(string connectionString)
            : base(connectionString)
        {
            SqlMapper.AddTypeHandler(typeof(Models.Data.GameState.State), new GameStateHandler());
            SqlMapper.AddTypeHandler(typeof(Models.Data.GameState.OpenState), new GameStateHandler());
            SqlMapper.AddTypeHandler(typeof(Models.Data.GameState.ClosedState), new GameStateHandler());
            SqlMapper.AddTypeHandler(typeof(Models.Data.GameState.ChatOnlyState), new GameStateHandler());
        }

        /// <summary>
        /// Adds the player asynchronous.
        /// </summary>
        /// <param name="playerGame">The player game.</param>
        /// <returns>
        /// Void result.
        /// </returns>
        public Task AddPlayerAsync(PlayerGame playerGame)
        {
            return this.CreateQueryAsync<PlayerGame>(playerGame);
        }

        /// <inheritdoc/>
        public async Task<List<Game>> GetPagedAsync(string alias, int page, int count, GameStateEnum gameState)
        {
            var gameSchema = GetSchema(typeof(Game));
            var pgSchema = GetSchema(typeof(PlayerGame));

            var query = $"SELECT {string.Join(", ", gameSchema.Columns.Select(p => $"{p.Name} as {p.Property}"))} " +
                $"FROM {gameSchema.TableName} g " +
                $"LEFT JOIN {pgSchema.TableName} pg ON g.id = pg.gameid " +
                $"WHERE g.state = {(int)gameState} AND pg.playerid = '{alias}' " +
                $"LIMIT {count} OFFSET {(page - 1) * count}";

            using (var conn = this.Connection)
            {
                conn.Open();
                var result = await conn.QueryAsync<Game>(query);
                conn.Close();

                return result.ToList();
            }
        }

        /// <inheritdoc/>
        public async override Task<Game> GetAsync(Guid key)
        {
            var gSchema = GetSchema(typeof(Game));
            var pSchema = GetSchema(typeof(Player));
            var pgSchema = GetSchema(typeof(PlayerGame));
            var mtSchema = GetSchema(typeof(MeetingTime));
            var pvSchema = GetSchema(typeof(PlayerVote));

            var mtDictionary = new Dictionary<Guid, MeetingTime>();
            var gameDictionary = new Dictionary<Guid, Game>();

            var gameAndPlayersQuery = $"SELECT * FROM {gSchema.SchemaName}.{gSchema.TableName} g " +
                $"LEFT JOIN {pgSchema.SchemaName}.{pgSchema.TableName} pg ON g.id = pg.gameid " +
                $"LEFT JOIN {pSchema.SchemaName}.{pSchema.TableName} p ON pg.playerid = p.id " +
                $"WHERE g.{gSchema.KeyName} = '{key}'";

            var mtAndPvQuery = $"SELECT * FROM {mtSchema.SchemaName}.{mtSchema.TableName} mt " +
                $"LEFT JOIN {pvSchema.SchemaName}.{pvSchema.TableName} pv ON mt.id = pv.meetingtimeid " +
                $"WHERE mt.gameid = '{key}'";

            using (var conn = this.Connection)
            {
                conn.Open();
                await conn.QueryAsync<Game, Player, Game>(
                    gameAndPlayersQuery,
                    (game, player) =>
                    {
                        if (!gameDictionary.TryGetValue(game.Id, out var gameEntity))
                        {
                            gameEntity = game;
                            gameEntity.PlayerGames = new List<PlayerGame>();
                            gameEntity.MeetingTimes = new List<MeetingTime>();
                            gameDictionary.Add(gameEntity.Id, gameEntity);
                        }

                        gameEntity.PlayerGames.Add(new PlayerGame { Game = gameEntity, GameId = gameEntity.Id, Player = player, PlayerId = player.Id });

                        return gameEntity;
                    });

                if (gameDictionary.Count == 0)
                {
                    return null;
                }

                await conn.QueryAsync<MeetingTime, PlayerVote, MeetingTime>(
                    mtAndPvQuery,
                    (mt, pv) =>
                    {
                        if (!mtDictionary.TryGetValue(mt.Id, out var mtEntity))
                        {
                            mtEntity = mt;
                            mtEntity.PlayerVotes = new List<PlayerVote>();
                            mtDictionary.Add(mtEntity.Id, mtEntity);
                        }

                        if (pv != null)
                        {
                            mtEntity.PlayerVotes.Add(pv);
                        }

                        return mtEntity;
                    });
                conn.Close();

                gameDictionary.First().Value.MeetingTimes.AddRange(mtDictionary.Values);

                return gameDictionary.First().Value;
            }
        }

        /// <inheritdoc/>
        public async Task<string> GetAdminId(Guid gameId)
        {
            var schema = GetSchema(typeof(Game));
            var query = $"SELECT adminid FROM {schema.SchemaName}.{schema.TableName} g" +
                $"WHERE g.{schema.KeyName} = '{gameId}'";

            using (var connection = this.Connection)
            {
                connection.Open();
                var result = await connection.QuerySingleAsync<string>(query);
                connection.Close();

                return result;
            }
        }

        public Task AddVoteAsync(PlayerVote vote)
        {
            return base.CreateQueryAsync<PlayerVote>(vote);
        }

        public async Task RemoveVoteAsync(Guid meetingTimeId, string playerId)
        {
            var schema = GetSchema(typeof(PlayerVote));

            using (var connection = this.Connection)
            {
                connection.Open();
                await connection.ExecuteAsync($"DELETE FROM {schema.SchemaName}.{schema.TableName} WHERE meetingtimeid = '{meetingTimeId}' AND playerid = '{playerId}'");
                connection.Close();
            }
        }
    }
}
