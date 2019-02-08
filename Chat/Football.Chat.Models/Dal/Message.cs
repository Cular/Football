using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Football.Chat.Models.Dal
{
    public class Message
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("gameId")]
        public Guid GameId { get; set; }

        [BsonElement("playerid")]
        public string PlayerId { get; set; }

        [BsonElement("body")]
        [MinLength(1, ErrorMessage = "Minimal length equals 1.")]
        [MaxLength(255, ErrorMessage = "Maximal lenght equals 255.")]
        public string Body { get; set; }

        [BsonElement("time")]
        public DateTimeOffset Time { get; set; }
    }
}
