using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class UserConversationConfiguration : IEntityTypeConfiguration<UserConversation>
{
    public void Configure(EntityTypeBuilder<UserConversation> builder)
    {
        builder.ToTable(nameof(UserConversation));

        #region Main properties
        builder.HasKey(x => new { x.UserId, x.ConversationId });

        builder.Property(x => x.NickName);
        builder.Property(x => x.AdderId);
        builder.Property(x => x.Status);
        builder.Property(x => x.IsRead);
        builder.Property(x => x.JoinedDate);
        builder.Property(x => x.LeaveDate);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserConversations)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.UserConversations)
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Adder)
            .WithMany()
            .HasForeignKey(x => x.AdderId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}
