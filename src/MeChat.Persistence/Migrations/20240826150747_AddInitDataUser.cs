using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class AddInitDataUser : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "User",
            columns: new[] { "Id", "Avatar", "CreatedBy", "CreatedDate", "DeleteAt", "Email", "Fullname", "IsDeleted", "ModifiedBy", "ModifiledDate", "Password", "RoldeId", "Status", "Username" },
            values: new object[] { new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), "https://cdnphoto.dantri.com.vn/YAfcu9nd4T5dX06hhpaf19_QvY8=/thumb_w/960/2021/05/15/co-gai-noi-nhu-con-vi-anh-can-cuoc-xinh-nhu-mong-nhan-sac-ngoai-doi-con-bat-ngo-hon-2-1621075314070.jpg", new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6163), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6207), new TimeSpan(0, 7, 0, 0, 0)), "mechat.mail@gmail.com", "test", false, new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"), new DateTimeOffset(new DateTime(2024, 8, 26, 22, 7, 47, 126, DateTimeKind.Unspecified).AddTicks(6199), new TimeSpan(0, 7, 0, 0, 0)), "test", 2, 1, "test" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("fafbf4a7-032e-4286-8beb-5d8aa66f6db9"));
    }
}

