// <copyright file="MeetingTimeRepository.cs" company="Yarik Home">
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
    using Microsoft.EntityFrameworkCore;
    using Models.Data;

    /// <summary>
    /// The meeting time repository.
    /// </summary>
    public class MeetingTimeRepository : BaseRepository<MeetingTime, Guid>, IMeetingTimeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingTimeRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connectionString.</param>
        public MeetingTimeRepository(string connectionString)
            : base(connectionString)
        {
        }

        public async Task<List<MeetingTime>> GetByGameId(Guid gameId)
        {
            var mtSchema = GetSchema(typeof(MeetingTime));
            var pvSchema = GetSchema(typeof(PlayerVote));

            var mtDictionary = new Dictionary<Guid, MeetingTime>();

            var query = $"SELECT * FROM {mtSchema.SchemaName}.{mtSchema.TableName} mt " +
                $"LEFT JOIN {pvSchema.SchemaName}.{pvSchema.TableName} pv ON mt.id = pv.meetingtimeid " +
                $"WHERE mt.gameid = '{gameId}'";

            using (var connection = this.Connection)
            {
                connection.Open();
                await connection.QueryAsync<MeetingTime, PlayerVote, MeetingTime>(
                    query,
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
                connection.Close();

                return mtDictionary.Values.ToList();
            }
        }
    }
}
