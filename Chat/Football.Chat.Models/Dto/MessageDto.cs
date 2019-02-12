using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Chat.Models.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public string Alias { get; set; }

        public string Body { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
