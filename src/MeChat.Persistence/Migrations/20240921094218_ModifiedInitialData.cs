using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class ModifiedInitialData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://me-chat.s3.ap-southeast-1.amazonaws.com/thanhtt.jpg", "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg", new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld02.jpg", "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg", new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld.jpg", "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg", new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)) });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(1000), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(1001), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(988), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(989), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "Friend",
            keyColumns: new[] { "UserFirstId", "UserSecondId" },
            keyValues: new object[] { new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"), new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19") },
            columns: new[] { "CreatedDate", "ModifiledDate" },
            values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(958), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 987, DateTimeKind.Unspecified).AddTicks(981), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.6435-9/51863877_2216876628639610_6964562136462786560_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=13d280&_nc_ohc=CkFWK4i94xMQ7kNvgFKmft2&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A6Zx5iuo9_o4-xw0tzRnM3L&oh=00_AYC9tQUQYhvdGa3-WzEBShl9D25MJbY_dPfAJx_vDHwlHA&oe=670DCD49", "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8394), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8395), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("a09c6cf6-710e-466f-e716-08dcd4f11f19"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-1/376711391_3582728322054427_6580417315416639743_n.jpg?stp=dst-jpg_s200x200&_nc_cat=111&ccb=1-7&_nc_sid=0ecb9b&_nc_ohc=DBrY6E9OFCkQ7kNvgFyeI5-&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A3dFMcRG-YVhj7w0WRNg1wT&oh=00_AYANj8xO-MOrS4l1FZE9wJycAe0ibFNv9ZGaextYUZegDg&oe=66EC242D", "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8391), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8392), new TimeSpan(0, 7, 0, 0, 0)) });

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Id",
            keyValue: new Guid("ed003c55-0557-4885-9055-c0c47cc4f7ab"),
            columns: new[] { "Avatar", "CoverPhoto", "CreatedDate", "ModifiledDate" },
            values: new object[] { "https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/458141307_3752163308376305_7786396549520709717_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=mZeMLHtxPS4Q7kNvgHz6M_O&_nc_ht=scontent.fhan2-4.fna&_nc_gid=AN95bCnQULrHiAXmIyVA2wZ&oh=00_AYC4yBc8ZZkdyQboPp8z4T0mdBHyc6PRqssrdLH_ahIDfQ&oe=66EC3E3A", "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600", new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8370), new TimeSpan(0, 7, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 21, 15, 1, 3, 988, DateTimeKind.Unspecified).AddTicks(8386), new TimeSpan(0, 7, 0, 0, 0)) });
    }
}
