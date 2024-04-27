using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                RoldeId = table.Column<int>(type: "int", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                OAuth2Status = table.Column<int>(type: "int", nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
                table.ForeignKey(
                    name: "FK_User_Role_RoldeId",
                    column: x => x.RoldeId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Role",
            columns: new[] { "Id", "DateCreated", "DateUpdated", "RoleName" },
            values: new object[,]
            {
                { 1, new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2591), new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2602), "Admin" },
                { 2, new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2603), new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2604), "User" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_User_RoldeId",
            table: "User",
            column: "RoldeId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "User");

        migrationBuilder.DropTable(
            name: "Role");
    }
}
