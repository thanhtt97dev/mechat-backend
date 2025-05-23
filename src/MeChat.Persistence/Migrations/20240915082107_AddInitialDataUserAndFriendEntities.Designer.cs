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
    [Migration("20240915082107_AddInitialDataUserAndFriendEntities")]
    partial class AddInitialDataUserAndFriendEntities
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

                    b.Property<int>("OldStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("SpecifierId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserFirstId", "UserSecondId");

                    b.HasIndex("OldStatus");

                    b.HasIndex("SpecifierId");

                    b.HasIndex("Status");

                    b.HasIndex("UserSecondId");

                    b.ToTable("Friend", (string)null);

                    b.HasData(
                        new
                        {
                            UserFirstId = new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                            UserSecondId = new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6883), new TimeSpan(0, 7, 0, 0, 0)),
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6914), new TimeSpan(0, 7, 0, 0, 0)),
                            OldStatus = 1,
                            SpecifierId = new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                            Status = 2
                        },
                        new
                        {
                            UserFirstId = new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                            UserSecondId = new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6921), new TimeSpan(0, 7, 0, 0, 0)),
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6922), new TimeSpan(0, 7, 0, 0, 0)),
                            OldStatus = 2,
                            SpecifierId = new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
                            Status = 3
                        },
                        new
                        {
                            UserFirstId = new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                            UserSecondId = new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6942), new TimeSpan(0, 7, 0, 0, 0)),
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)),
                            OldStatus = 2,
                            SpecifierId = new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                            Status = 3
                        });
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
                            Avatar = "https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/458141307_3752163308376305_7786396549520709717_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=mZeMLHtxPS4Q7kNvgHz6M_O&_nc_ht=scontent.fhan2-4.fna&_nc_gid=AN95bCnQULrHiAXmIyVA2wZ&oh=00_AYC4yBc8ZZkdyQboPp8z4T0mdBHyc6PRqssrdLH_ahIDfQ&oe=66EC3E3A",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2505), new TimeSpan(0, 7, 0, 0, 0)),
                            Email = "mechat.mail@gmail.com",
                            Fullname = "test",
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2526), new TimeSpan(0, 7, 0, 0, 0)),
                            Password = "test",
                            RoleId = 2,
                            Status = 1,
                            Username = "test"
                        },
                        new
                        {
                            Id = new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
                            Avatar = "https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-1/376711391_3582728322054427_6580417315416639743_n.jpg?stp=dst-jpg_s200x200&_nc_cat=111&ccb=1-7&_nc_sid=0ecb9b&_nc_ohc=DBrY6E9OFCkQ7kNvgFyeI5-&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A3dFMcRG-YVhj7w0WRNg1wT&oh=00_AYANj8xO-MOrS4l1FZE9wJycAe0ibFNv9ZGaextYUZegDg&oe=66EC242D",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2532), new TimeSpan(0, 7, 0, 0, 0)),
                            Email = "leduchieu2001x@gmail.com",
                            Fullname = "test1",
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2533), new TimeSpan(0, 7, 0, 0, 0)),
                            Password = "test1",
                            RoleId = 2,
                            Status = 1,
                            Username = "test1"
                        },
                        new
                        {
                            Id = new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                            Avatar = "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.6435-9/51863877_2216876628639610_6964562136462786560_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=13d280&_nc_ohc=CkFWK4i94xMQ7kNvgFKmft2&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A6Zx5iuo9_o4-xw0tzRnM3L&oh=00_AYC9tQUQYhvdGa3-WzEBShl9D25MJbY_dPfAJx_vDHwlHA&oe=670DCD49",
                            CreatedDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2537), new TimeSpan(0, 7, 0, 0, 0)),
                            Email = "hieuldhe150703@fpt.edu.vn",
                            Fullname = "test2",
                            ModifiledDate = new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2539), new TimeSpan(0, 7, 0, 0, 0)),
                            Password = "test2",
                            RoleId = 2,
                            Status = 1,
                            Username = "test2"
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
                    b.HasOne("MeChat.Domain.Entities.FriendStatus", "OldFriendStatus")
                        .WithMany()
                        .HasForeignKey("OldStatus")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.User", "Specifier")
                        .WithMany()
                        .HasForeignKey("SpecifierId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MeChat.Domain.Entities.FriendStatus", "FriendStatus")
                        .WithMany("Friends")
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Restrict)
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

                    b.Navigation("OldFriendStatus");

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
