﻿// <auto-generated />
using System;
using BattleShipAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BattleShipAPI.Migrations
{
    [DbContext(typeof(BattleShipDbContext))]
    partial class BattleShipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BattleShipAPI.Entities.Cordiantes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cord")
                        .HasColumnType("int");

                    b.Property<int>("ShipId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShipId");

                    b.ToTable("Cordiantes");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LastMoveBoardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.GameBoard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("GameBoards");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Ship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSunk")
                        .HasColumnType("bit");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Cordiantes", b =>
                {
                    b.HasOne("BattleShipAPI.Entities.Ship", "Ship")
                        .WithMany("Cordinates")
                        .HasForeignKey("ShipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ship");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.GameBoard", b =>
                {
                    b.HasOne("BattleShipAPI.Entities.Game", "Game")
                        .WithMany("GameBoard")
                        .HasForeignKey("GameId");

                    b.HasOne("BattleShipAPI.Entities.Player", "Player")
                        .WithOne("GameBoard")
                        .HasForeignKey("BattleShipAPI.Entities.GameBoard", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Ship", b =>
                {
                    b.HasOne("BattleShipAPI.Entities.GameBoard", "Board")
                        .WithMany("Ships")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Game", b =>
                {
                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.GameBoard", b =>
                {
                    b.Navigation("Ships");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Player", b =>
                {
                    b.Navigation("GameBoard");
                });

            modelBuilder.Entity("BattleShipAPI.Entities.Ship", b =>
                {
                    b.Navigation("Cordinates");
                });
#pragma warning restore 612, 618
        }
    }
}
