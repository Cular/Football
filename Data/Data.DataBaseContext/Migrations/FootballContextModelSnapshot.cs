﻿// <auto-generated />
using System;
using Data.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.DataBaseContext.Migrations
{
    [DbContext(typeof(FootballContext))]
    partial class FootballContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Data.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("FriendId")
                        .HasColumnName("friendid");

                    b.Property<bool>("IsApproved")
                        .HasColumnName("isapproved");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnName("playerid");

                    b.HasKey("Id");

                    b.HasIndex("FriendId");

                    b.HasIndex("PlayerId");

                    b.ToTable("friendships");
                });

            modelBuilder.Entity("Models.Data.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasColumnName("adminid");

                    b.Property<int>("State")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("games");
                });

            modelBuilder.Entity("Models.Data.MeetingTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid>("GameId")
                        .HasColumnName("gameid");

                    b.Property<bool>("IsChoosen")
                        .HasColumnName("ischoosen");

                    b.Property<DateTimeOffset>("TimeOfMeet")
                        .HasColumnName("timeofmeet");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("meetingtimes");
                });

            modelBuilder.Entity("Models.Data.Player", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("passwordhash");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("players");
                });

            modelBuilder.Entity("Models.Data.PlayerActivation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnName("playerid");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("playeractivations");
                });

            modelBuilder.Entity("Models.Data.PlayerGame", b =>
                {
                    b.Property<string>("PlayerId")
                        .HasColumnName("playerid");

                    b.Property<Guid>("GameId")
                        .HasColumnName("gameid");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("playersgames");
                });

            modelBuilder.Entity("Models.Data.PlayerVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<Guid>("MeetingTimeId")
                        .HasColumnName("meetingtimeid");

                    b.Property<string>("PlayerId")
                        .HasColumnName("playerid");

                    b.HasKey("Id");

                    b.HasIndex("MeetingTimeId");

                    b.ToTable("playervotes");
                });

            modelBuilder.Entity("Models.Data.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .HasColumnName("active");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnName("token");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("refreshtokens");
                });

            modelBuilder.Entity("Models.Data.Friendship", b =>
                {
                    b.HasOne("Models.Data.Player", "Friend")
                        .WithMany()
                        .HasForeignKey("FriendId");

                    b.HasOne("Models.Data.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Data.Game", b =>
                {
                    b.HasOne("Models.Data.Player", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Models.Data.MeetingTime", b =>
                {
                    b.HasOne("Models.Data.Game", "Game")
                        .WithMany("MeetingTimes")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Data.PlayerActivation", b =>
                {
                    b.HasOne("Models.Data.Player", "Player")
                        .WithOne()
                        .HasForeignKey("Models.Data.PlayerActivation", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Data.PlayerGame", b =>
                {
                    b.HasOne("Models.Data.Game", "Game")
                        .WithMany("PlayerGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Data.Player", "Player")
                        .WithMany("PlayerGames")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Data.PlayerVote", b =>
                {
                    b.HasOne("Models.Data.MeetingTime", "MeetingTime")
                        .WithMany("PlayerVotes")
                        .HasForeignKey("MeetingTimeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
