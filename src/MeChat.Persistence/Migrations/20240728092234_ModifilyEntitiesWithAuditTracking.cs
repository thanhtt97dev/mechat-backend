using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifilyEntitiesWithAuditTracking : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "DateUpdated",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "User");

        migrationBuilder.DropColumn(
            name: "DateUpdated",
            table: "User");

        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "Social");

        migrationBuilder.DropColumn(
            name: "DateUpdated",
            table: "Social");

        migrationBuilder.DropColumn(
            name: "DateCreated",
            table: "Role");

        migrationBuilder.DropColumn(
            name: "DateUpdated",
            table: "Role");

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedBy",
            table: "UserSocial",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "CreatedDate",
            table: "UserSocial",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "ModifiledDate",
            table: "UserSocial",
            type: "datetimeoffset",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedBy",
            table: "User",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "CreatedDate",
            table: "User",
            type: "datetimeoffset",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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

        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "ModifiledDate",
            table: "User",
            type: "datetimeoffset",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
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
            name: "ModifiledDate",
            table: "UserSocial");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "User");

        migrationBuilder.DropColumn(
            name: "CreatedDate",
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

        migrationBuilder.DropColumn(
            name: "ModifiledDate",
            table: "User");

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "UserSocial",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateUpdated",
            table: "UserSocial",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "User",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateUpdated",
            table: "User",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "Social",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateUpdated",
            table: "Social",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateCreated",
            table: "Role",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<DateTime>(
            name: "DateUpdated",
            table: "Role",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        migrationBuilder.UpdateData(
            table: "Social",
            keyColumn: "Id",
            keyValue: 1,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1679), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1682) });

        migrationBuilder.UpdateData(
            table: "Social",
            keyColumn: "Id",
            keyValue: 2,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1684), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1685) });

        migrationBuilder.UpdateData(
            table: "Social",
            keyColumn: "Id",
            keyValue: 3,
            columns: new[] { "DateCreated", "DateUpdated" },
            values: new object[] { new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1686), new DateTime(2024, 7, 3, 8, 10, 52, 922, DateTimeKind.Local).AddTicks(1686) });
    }
}
