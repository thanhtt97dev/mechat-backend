using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifyNotificationEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "ModifiledDate",
            table: "Notification");

        migrationBuilder.DropColumn(
            name: "Title",
            table: "Notification");

        migrationBuilder.AlterColumn<DateTime>(
            name: "CreatedDate",
            table: "Notification",
            type: "datetime2",
            nullable: false,
            oldClrType: typeof(DateTimeOffset),
            oldType: "datetimeoffset");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTimeOffset>(
            name: "CreatedDate",
            table: "Notification",
            type: "datetimeoffset",
            nullable: false,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "ModifiledDate",
            table: "Notification",
            type: "datetimeoffset",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Title",
            table: "Notification",
            type: "nvarchar(max)",
            nullable: true);
    }
}
