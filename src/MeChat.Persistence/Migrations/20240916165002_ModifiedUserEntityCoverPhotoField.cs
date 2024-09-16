using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifiedUserEntityCoverPhotoField : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CoverPhoto",
            table: "User",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4185), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4186), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4179), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4180), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4138), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 764, DateTimeKind.Unspecified).AddTicks(4170), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
            columns: new[] { "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2924), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2924), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
            columns: new[] { "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2919), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2920), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2886), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 16, 23, 50, 1, 766, DateTimeKind.Unspecified).AddTicks(2913), new TimeSpan(0, 7, 0, 0, 0)) });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CoverPhoto",
            table: "User");

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6942), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6921), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6922), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6883), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6914), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2537), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2539), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2532), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2533), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2505), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2526), new TimeSpan(0, 7, 0, 0, 0)) });
    }
}
