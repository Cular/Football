using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Chat.Models.Dto
{
    public class MessageCreateDto
    {
        public Guid GameId { get; set; }

        public string Body { get; set; }
    }
}
