using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeChat.Persistence.Migrations;

/// <inheritdoc />
public partial class AddConversationAndMessageEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Conversation",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Type = table.Column<int>(type: "int", nullable: false),
                Accessibility = table.Column<int>(type: "int", nullable: false),
                AdministratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                ModifiledDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Conversation", x => x.Id);
                table.ForeignKey(
                    name: "FK_Conversation_User_AdministratorId",
                    column: x => x.AdministratorId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Message",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Type = table.Column<int>(type: "int", nullable: false),
                IsDelete = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                ModifiledDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Message", x => x.Id);
                table.ForeignKey(
                    name: "FK_Message_Conversation_ConversationId",
                    column: x => x.ConversationId,
                    principalTable: "Conversation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Message_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "UserConversation",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ConversationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                AdderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false),
                IsRead = table.Column<bool>(type: "bit", nullable: false),
                JoinedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                LeaveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserConversation", x => new { x.UserId, x.ConversationId });
                table.ForeignKey(
                    name: "FK_UserConversation_Conversation_ConversationId",
                    column: x => x.ConversationId,
                    principalTable: "Conversation",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserConversation_User_AdderId",
                    column: x => x.AdderId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_UserConversation_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Conversation_AdministratorId",
            table: "Conversation",
            column: "AdministratorId");

        migrationBuilder.CreateIndex(
            name: "IX_Message_ConversationId",
            table: "Message",
            column: "ConversationId");

        migrationBuilder.CreateIndex(
            name: "IX_Message_UserId",
            table: "Message",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserConversation_AdderId",
            table: "UserConversation",
            column: "AdderId");

        migrationBuilder.CreateIndex(
            name: "IX_UserConversation_ConversationId",
            table: "UserConversation",
            column: "ConversationId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Message");

        migrationBuilder.DropTable(
            name: "UserConversation");

        migrationBuilder.DropTable(
            name: "Conversation");
    }
}
