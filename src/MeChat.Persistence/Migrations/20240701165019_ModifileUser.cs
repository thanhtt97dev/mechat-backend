using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifileUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Fullname",
            table: "User",
            type: "nvarchar(max)",
            nullable: true);

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Fullname",
            table: "User");

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2591), new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2602) });

        migrationBuilder.UpdateData(
            table: "Role",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2603), new DateTime(2024, 4, 27, 11, 52, 7, 638, DateTimeKind.Local).AddTicks(2604) });
    }
}
