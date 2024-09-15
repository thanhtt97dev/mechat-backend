using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class AddInitialDataUserAndFriendEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "Avatar", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/458141307_3752163308376305_7786396549520709717_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=mZeMLHtxPS4Q7kNvgHz6M_O&_nc_ht=scontent.fhan2-4.fna&_nc_gid=AN95bCnQULrHiAXmIyVA2wZ&oh=00_AYC4yBc8ZZkdyQboPp8z4T0mdBHyc6PRqssrdLH_ahIDfQ&oe=66EC3E3A", new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2505), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2526), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.InsertData(
            table: "User",
            columns: new[] { "Id", "Avatar", "CreatedDate", "Email", "Fullname", "ModifiledDate", "Password", "RoleId", "Status", "Username" },
            values: new object[,]
            {
                { new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"), "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.6435-9/51863877_2216876628639610_6964562136462786560_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=13d280&_nc_ohc=CkFWK4i94xMQ7kNvgFKmft2&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A6Zx5iuo9_o4-xw0tzRnM3L&oh=00_AYC9tQUQYhvdGa3-WzEBShl9D25MJbY_dPfAJx_vDHwlHA&oe=670DCD49", new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2537), new TimeSpan(0, 7, 0, 0, 0)), "hieuldhe150703@fpt.edu.vn", "test2", new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2539), new TimeSpan(0, 7, 0, 0, 0)), "test2", 2, 1, "test2" },
                { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), "https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-1/376711391_3582728322054427_6580417315416639743_n.jpg?stp=dst-jpg_s200x200&_nc_cat=111&ccb=1-7&_nc_sid=0ecb9b&_nc_ohc=DBrY6E9OFCkQ7kNvgFyeI5-&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A3dFMcRG-YVhj7w0WRNg1wT&oh=00_AYANj8xO-MOrS4l1FZE9wJycAe0ibFNv9ZGaextYUZegDg&oe=66EC242D", new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2532), new TimeSpan(0, 7, 0, 0, 0)), "leduchieu2001x@gmail.com", "test1", new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 34, DateTimeKind.Unspecified).AddTicks(2533), new TimeSpan(0, 7, 0, 0, 0)), "test1", 2, 1, "test1" }
            });

        migrationBuilder.InsertData(
            table: "Friend",
            columns: new[] { "UserFirstId", "UserSecondId", "CreatedDate", "ModifiledDate", "OldStatus", "SpecifierId", "Status" },
            values: new object[,]
            {
                { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6942), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6943), new TimeSpan(0, 7, 0, 0, 0)), 2, new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), 3 },
                { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6921), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6922), new TimeSpan(0, 7, 0, 0, 0)), 2, new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), 3 },
                { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6883), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 15, 21, 7, 32, DateTimeKind.Unspecified).AddTicks(6914), new TimeSpan(0, 7, 0, 0, 0)), 1, new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), 2 }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") });

        migrationBuilder.DeleteData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") });

        migrationBuilder.DeleteData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19") });

        migrationBuilder.DeleteData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"));

        migrationBuilder.DeleteData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"));

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "Avatar", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://cdnphoto.dantri.com.vn/YAfcu9nd4T5dX06hhpaf19_QvY8=/thumb_w/960/2021/05/15/co-gai-noi-nhu-con-vi-anh-can-cuoc-xinh-nhu-mong-nhan-sac-ngoai-doi-con-bat-ngo-hon-2-1621075314070.jpg", new DateTimeOffset(new DateTime(2024, 9, 15, 1, 46, 35, 372, DateTimeKind.Unspecified).AddTicks(6490), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 1, 46, 35, 372, DateTimeKind.Unspecified).AddTicks(6515), new TimeSpan(0, 7, 0, 0, 0)) });
    }
}
