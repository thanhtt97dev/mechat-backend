using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifiledInitDataFriendStatusEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 1,
            column: "Name",
            value: "UnFriend");

        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 2,
            column: "Name",
            value: "Waiting accept");

        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 3,
            column: "Name",
            value: "Accepted");

        migrationBuilder.InsertData(
            table: "FriendStatus",
            columns: new[] { "Id", "Name" },
            values: new object[] { 4, "Block" });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3828), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3862), new TimeSpan(0, 7, 0, 0, 0)) });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 4);

        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 1,
            column: "Name",
            value: "Waiting accept");

        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 2,
            column: "Name",
            value: "Accepted");

        migrationBuilder.UpdateData(
            table: "FriendStatus",
            keyColumn: "Id",
            keyValue: 3,
            column: "Name",
            value: "Block");

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 14, 0, 56, 30, 829, DateTimeKind.Unspecified).AddTicks(8877), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 14, 0, 56, 30, 829, DateTimeKind.Unspecified).AddTicks(8907), new TimeSpan(0, 7, 0, 0, 0)) });
    }
}
