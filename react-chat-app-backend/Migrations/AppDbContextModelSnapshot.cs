﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using react_chat_app_backend.Context;

#nullable disable

namespace react_chat_app_backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("react_chat_app_backend.Models.MessageData", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("date")
                        .HasColumnType("TEXT");

                    b.Property<string>("receiverId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("senderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("type")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("react_chat_app_backend.Models.UserData", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("TEXT");

                    b.Property<string>("lastMessage")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("photoURL")
                        .HasColumnType("TEXT");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("react_chat_app_backend.Models.UserFriendShip", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RelatedUserId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("isPending")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RelatedUserId");

                    b.HasIndex("RelatedUserId");

                    b.ToTable("UserFriendShips");
                });

            modelBuilder.Entity("react_chat_app_backend.Models.UserFriendShip", b =>
                {
                    b.HasOne("react_chat_app_backend.Models.UserData", "RelatedUser")
                        .WithMany()
                        .HasForeignKey("RelatedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("react_chat_app_backend.Models.UserData", "User")
                        .WithMany("UserFriendShips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RelatedUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("react_chat_app_backend.Models.UserData", b =>
                {
                    b.Navigation("UserFriendShips");
                });
#pragma warning restore 612, 618
        }
    }
}
