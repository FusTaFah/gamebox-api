﻿// <auto-generated />
using System;
using GameBox.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameBox.Migrations
{
    [DbContext(typeof(GameBoxContext))]
    [Migration("20241116035644_AddEmailAddressToUser")]
    partial class AddEmailAddressToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameBox.Models.DB.CollectionEntry", b =>
                {
                    b.Property<int>("CollectionEntryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollectionEntryID"));

                    b.Property<DateTime>("Added")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("PlatformID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CollectionEntryID");

                    b.HasIndex("GameID");

                    b.HasIndex("PlatformID");

                    b.HasIndex("UserID");

                    b.ToTable("CollectionEntry");
                });

            modelBuilder.Entity("GameBox.Models.DB.Game", b =>
                {
                    b.Property<int>("GameID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameID");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameBox.Models.DB.GamePlatform", b =>
                {
                    b.Property<int>("GamePlatformID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GamePlatformID"));

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<int>("PlatformID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("GamePlatformID");

                    b.HasIndex("GameID");

                    b.HasIndex("PlatformID");

                    b.ToTable("GamePlatform");
                });

            modelBuilder.Entity("GameBox.Models.DB.Platform", b =>
                {
                    b.Property<int>("PlatformID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlatformID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlatformID");

                    b.ToTable("Platform");
                });

            modelBuilder.Entity("GameBox.Models.DB.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameBox.Models.DB.CollectionEntry", b =>
                {
                    b.HasOne("GameBox.Models.DB.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBox.Models.DB.Platform", "Platform")
                        .WithMany()
                        .HasForeignKey("PlatformID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBox.Models.DB.User", "User")
                        .WithMany("CollectionEntries")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameBox.Models.DB.GamePlatform", b =>
                {
                    b.HasOne("GameBox.Models.DB.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameBox.Models.DB.Platform", "Platform")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GameBox.Models.DB.Game", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("GameBox.Models.DB.Platform", b =>
                {
                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("GameBox.Models.DB.User", b =>
                {
                    b.Navigation("CollectionEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
