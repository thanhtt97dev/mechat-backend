using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                RoleName = table.Column<int>(type: "int", maxLength: 100, nullable: false),
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
                Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                RoldeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
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
