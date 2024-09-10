using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifiedUserEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_User_Role_RoldeId",
            table: "User");

        migrationBuilder.DeleteData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"));

        migrationBuilder.RenameColumn(
            name: "RoldeId",
            table: "User",
            newName: "RoleId");

        migrationBuilder.RenameIndex(
            name: "IX_User_RoldeId",
            table: "User",
            newName: "IX_User_RoleId");

        migrationBuilder.InsertData(
            table: "User",
            columns: new[] { "Id", "Avatar", "CreatedBy", "CreatedDate", "DeleteAt", "Email", "Fullname", "IsDeleted", "ModifiedBy", "ModifiledDate", "Password", "RoleId", "Status", "Username" },
            values: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), "https://cdnphoto.dantri.com.vn/YAfcu9nd4T5dX06hhpaf19_QvY8=/thumb_w/960/2021/05/15/co-gai-noi-nhu-con-vi-anh-can-cuoc-xinh-nhu-mong-nhan-sac-ngoai-doi-con-bat-ngo-hon-2-1621075314070.jpg", new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(2), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(37), new TimeSpan(0, 7, 0, 0, 0)), "mechat.mail@gmail.com", "test", false, new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new DateTimeOffset(new DateTime(2024, 9, 9, 21, 55, 57, 363, DateTimeKind.Unspecified).AddTicks(32), new TimeSpan(0, 7, 0, 0, 0)), "test", 2, 1, "test" });

        migrationBuilder.AddForeignKey(
            name: "FK_User_Role_RoleId",
            table: "User",
            column: "RoleId",
            principalTable: "Role",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_User_Role_RoleId",
            table: "User");

        migrationBuilder.DeleteData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"));

        migrationBuilder.RenameColumn(
            name: "RoleId",
            table: "User",
            newName: "RoldeId");

        migrationBuilder.RenameIndex(
            name: "IX_User_RoleId",
            table: "User",
            newName: "IX_User_RoldeId");

        migrationBuilder.InsertData(
            table: "User",
            columns: new[] { "Id", "Avatar", "CreatedBy", "CreatedDate", "DeleteAt", "Email", "Fullname", "IsDeleted", "ModifiedBy", "ModifiledDate", "Password", "RoldeId", "Status", "Username" },
            values: new object[] { new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), "https://cdnphoto.dantri.com.vn/YAfcu9nd4T5dX06hhpaf19_QvY8=/thumb_w/960/2021/05/15/co-gai-noi-nhu-con-vi-anh-can-cuoc-xinh-nhu-mong-nhan-sac-ngoai-doi-con-bat-ngo-hon-2-1621075314070.jpg", new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6163), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6207), new TimeSpan(0, 7, 0, 0, 0)), "mechat.mail@gmail.com", "test", false, new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6199), new TimeSpan(0, 7, 0, 0, 0)), "test", 2, 1, "test" });

        migrationBuilder.AddForeignKey(
            name: "FK_User_Role_RoldeId",
            table: "User",
            column: "RoldeId",
            principalTable: "Role",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
