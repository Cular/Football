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
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Data.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("AdminId")
                        .HasColumnName("adminid");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("games");
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

            modelBuilder.Entity("Models.Data.Game", b =>
                {
                    b.HasOne("Models.Data.Player", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Models.Data.PlayerActivation", b =>
                {
                    b.HasOne("Models.Data.Player", "Player")
                        .WithOne()
                        .HasForeignKey("Models.Data.PlayerActivation", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
