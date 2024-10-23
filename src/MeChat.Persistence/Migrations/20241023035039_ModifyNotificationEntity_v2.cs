using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifyNotificationEntity_v2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Notification_User_UserId",
            table: "Notification");

        migrationBuilder.DropColumn(
            name: "Content",
            table: "Notification");

        migrationBuilder.DropColumn(
            name: "Image",
            table: "Notification");

        migrationBuilder.RenameColumn(
            name: "UserId",
            table: "Notification",
            newName: "RequesterId");

        migrationBuilder.RenameIndex(
            name: "IX_Notification_UserId",
            table: "Notification",
            newName: "IX_Notification_RequesterId");

        migrationBuilder.AddColumn<Guid>(
            name: "ReceiverId",
            table: "Notification",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<int>(
            name: "Type",
            table: "Notification",
            type: "int",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.CreateIndex(
            name: "IX_Notification_ReceiverId",
            table: "Notification",
            column: "ReceiverId");

        migrationBuilder.AddForeignKey(
            name: "FK_Notification_User_ReceiverId",
            table: "Notification",
            column: "ReceiverId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_Notification_User_RequesterId",
            table: "Notification",
            column: "RequesterId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Notification_User_ReceiverId",
            table: "Notification");

        migrationBuilder.DropForeignKey(
            name: "FK_Notification_User_RequesterId",
            table: "Notification");

        migrationBuilder.DropIndex(
            name: "IX_Notification_ReceiverId",
            table: "Notification");

        migrationBuilder.DropColumn(
            name: "ReceiverId",
            table: "Notification");

        migrationBuilder.DropColumn(
            name: "Type",
            table: "Notification");

        migrationBuilder.RenameColumn(
            name: "RequesterId",
            table: "Notification",
            newName: "UserId");

        migrationBuilder.RenameIndex(
            name: "IX_Notification_RequesterId",
            table: "Notification",
            newName: "IX_Notification_UserId");

        migrationBuilder.AddColumn<string>(
            name: "Content",
            table: "Notification",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Image",
            table: "Notification",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddForeignKey(
            name: "FK_Notification_User_UserId",
            table: "Notification",
            column: "UserId",
            principalTable: "User",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
