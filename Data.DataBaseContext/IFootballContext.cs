using Microsoft.EntityFrameworkCore;
using Models.Data;
using System;

namespace Data.DataBaseContext
{
    public interface IFootballContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Player> Players { get; set; }
    }
}
