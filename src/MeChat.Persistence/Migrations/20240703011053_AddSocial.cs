using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class AddSocial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "OAuth2Status",
            table: "User");

        migrationBuilder.CreateTable(
            name: "Social",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Social", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserSocial",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SocialId = table.Column<int>(type: "int", nullable: false),
                AccountSocialId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserSocial", x => new { x.UserId, x.SocialId });
                table.ForeignKey(
                    name: "FK_UserSocial_Social_SocialId",
                    column: x => x.SocialId,
                    principalTable: "Social",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserSocial_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 3, 8, 10, 52, 921, DateTimeKind.Local).AddTicks(9142), new DateTime(2024, 7, 3, 8, 10, 52, 921, DateTimeKind.Local).AddTicks(9154) });

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 3, 8, 10, 52, 921, DateTimeKind.Local).AddTicks(9156), new DateTime(2024, 7, 3, 8, 10, 52, 921, DateTimeKind.Local).AddTicks(9156) });

        migrationBuilder.InsertData(
            table: "Social",
            columns: new[] { "Id", "DateCreated", "DateUpdated", "Name" },
            values: new object[,]
            {
                { 1, new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1679), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1682), "Google" },
                { 2, new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1684), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1685), "Facebook" },
                { 3, new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1686), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1686), "Git" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_UserSocial_SocialId",
            table: "UserSocial",
            column: "SocialId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserSocial");

        migrationBuilder.DropTable(
            name: "Social");

        migrationBuilder.AddColumn<int>(
            name: "OAuth2Status",
            table: "User",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 1, 23, 50, 18, 980, DateTimeKind.Local).AddTicks(5199), new DateTime(2024, 7, 1, 23, 50, 18, 980, DateTimeKind.Local).AddTicks(5241) });

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 1, 23, 50, 18, 980, DateTimeKind.Local).AddTicks(5244), new DateTime(2024, 7, 1, 23, 50, 18, 980, DateTimeKind.Local).AddTicks(5244) });
    }
}
