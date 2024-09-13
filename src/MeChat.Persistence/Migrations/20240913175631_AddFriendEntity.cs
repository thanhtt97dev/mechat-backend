using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class AddFriendEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "DeleteAt",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "ModifiedBy",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "User");

        migrationBuilder.DropColumn(
            name: "DeleteAt",
            table: "User");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "User");

        migrationBuilder.DropColumn(
            name: "ModifiedBy",
            table: "User");

        migrationBuilder.CreateTable(
            name: "FriendStatus",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FriendStatus", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Friend",
            columns: table => new
            {
                UserFirstId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserSecondId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SpecifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                ModifiledDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Friend", x => new { x.UserFirstId, x.UserSecondId });
                table.ForeignKey(
                    name: "FK_Friend_FriendStatus_Status",
                    column: x => x.Status,
                    principalTable: "FriendStatus",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Friend_User_SpecifierId",
                    column: x => x.SpecifierId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Friend_User_UserFirstId",
                    column: x => x.UserFirstId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Friend_User_UserSecondId",
                    column: x => x.UserSecondId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "FriendStatus",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Waiting accept" },
                { 2, "Accepted" },
                { 3, "Block" }
            });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 14, 0, 56, 30, 829, DateTimeKind.Unspecified).AddTicks(8877), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 14, 0, 56, 30, 829, DateTimeKind.Unspecified).AddTicks(8907), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.CreateIndex(
            name: "IX_Friend_SpecifierId",
            table: "Friend",
            column: "SpecifierId");

        migrationBuilder.CreateIndex(
            name: "IX_Friend_Status",
            table: "Friend",
            column: "Status");

        migrationBuilder.CreateIndex(
            name: "IX_Friend_UserSecondId",
            table: "Friend",
            column: "UserSecondId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Friend");

        migrationBuilder.DropTable(
            name: "FriendStatus");

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedBy",
            table: "UserSocial",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "DeleteAt",
            table: "UserSocial",
            type: "datetimeoffset",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "UserSocial",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedBy",
            table: "UserSocial",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedBy",
            table: "User",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "DeleteAt",
            table: "User",
            type: "datetimeoffset",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "User",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedBy",
            table: "User",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "CreatedBy", "CreatedDate", "DeleteAt", "IsDeleted", "ModifiedBy", "ModifiledDate" },
            values: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(2), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(37), new TimeSpan(0, 7, 0, 0, 0)), false, new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(32), new TimeSpan(0, 7, 0, 0, 0)) });
    }
}
