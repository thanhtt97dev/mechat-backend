using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifiledFriendEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Friend_FriendStatus_Status",
            table: "Friend");

        migrationBuilder.AddColumn<int>(
            name: "OldStatus",
            table: "Friend",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 15, 1, 46, 35, 372, DateTimeKind.Unspecified).AddTicks(6490), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 1, 46, 35, 372, DateTimeKind.Unspecified).AddTicks(6515), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.CreateIndex(
            name: "IX_Friend_OldStatus",
            table: "Friend",
            column: "OldStatus");

        migrationBuilder.AddForeignKey(
            name: "FK_Friend_FriendStatus_OldStatus",
            table: "Friend",
            column: "OldStatus",
            principalTable: "FriendStatus",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Friend_FriendStatus_Status",
            table: "Friend",
            column: "Status",
            principalTable: "FriendStatus",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Friend_FriendStatus_OldStatus",
            table: "Friend");

        migrationBuilder.DropForeignKey(
            name: "FK_Friend_FriendStatus_Status",
            table: "Friend");

        migrationBuilder.DropIndex(
            name: "IX_Friend_OldStatus",
            table: "Friend");

        migrationBuilder.DropColumn(
            name: "OldStatus",
            table: "Friend");

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3828), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 14, 10, 19, 28, 939, DateTimeKind.Unspecified).AddTicks(3862), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.AddForeignKey(
            name: "FK_Friend_FriendStatus_Status",
            table: "Friend",
            column: "Status",
            principalTable: "FriendStatus",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
