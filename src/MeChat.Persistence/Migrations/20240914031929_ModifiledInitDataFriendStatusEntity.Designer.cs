﻿// <auto-generated />
using System;
using MeChat.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeChat.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240914031929_ModifiledInitDataFriendStatusEntity")]
    partial class ModifiledInitDataFriendStatusEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeChat.Domain.Entities.Friend", b =>
                {
                    b.Property<Guid>("UserFirstId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserSecondId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiledDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("SpecifierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserFirstId", "UserSecondId");

                    b.HasIndex("SpecifierId");

                    b.HasIndex("Status");

                    b.HasIndex("UserSecondId");

                    b.ToTable("Friend", (string)null);
                });

            modelBuilder.Entity("MeChat.Domain.Entities.FriendStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FriendStatus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "UnFriend"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Waiting accept"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Block"
                        });
                });

            modelBuilder.Entity("MeChat.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("MeChat.Domain.Entities.Social", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Social", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Google"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Facebook"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Git"
                        });
                });

            modelBuilder.Entity("MeChat.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("ModifiledDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                            Avatar = "https://cdnphoto.dantri.com.vn/YAfcu9nd4T5dX06hhpaf19_QvY8=/thumb_w/960/2021/05/15/co-gai-noi-nhu-con-vi-anh-can-cuoc-xinh-nhu-mong-nhan-sac-ngoai-doi-con-bat-ngo-hon-2-1621075314070.jpg",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3828), new TimeSpan(0, 7, 0, 0, 0)),
                            Email = "mechat.mail@gmail.com",
                            Fullname = "test",
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3862), new TimeSpan(0, 7, 0, 0, 0)),
                            Password = "test",
                            RoleId = 2,
                            Status = 1,
                            Username = "test"
                        });
                });

            modelBuilder.Entity("MeChat.Domain.Entities.UserSocial", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SocialId")
                        .HasColumnType("int");

                    b.Property<string>("AccountSocialId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("ModifiledDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserId", "SocialId");

                    b.HasIndex("SocialId");

                    b.ToTable("UserSocial", (string)null);
                });

            modelBuilder.Entity("MeChat.Domain.Entities.Friend", b =>
                {
                    b.HasOne("MeChat.Domain.Entities.User", "Specifier")
                        .WithMany()
                        .HasForeignKey("SpecifierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.FriendStatus", "FriendStatus")
                        .WithMany("Friends")
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.User", "UserFirst")
                        .WithMany("Friends")
                        .HasForeignKey("UserFirstId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.User", "UserSecond")
                        .WithMany()
                        .HasForeignKey("UserSecondId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FriendStatus");

                    b.Navigation("Specifier");

                    b.Navigation("UserFirst");

                    b.Navigation("UserSecond");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.User", b =>
                {
                    b.HasOne("MeChat.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.UserSocial", b =>
                {
                    b.HasOne("MeChat.Domain.Entities.Social", "Social")
                        .WithMany("UserSocials")
                        .HasForeignKey("SocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.User", "User")
                        .WithMany("UserSocials")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Social");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.FriendStatus", b =>
                {
                    b.Navigation("Friends");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.Social", b =>
                {
                    b.Navigation("UserSocials");
                });

            modelBuilder.Entity("MeChat.Domain.Entities.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("UserSocials");
                });
#pragma warning restore 612, 618
        }
    }
}
