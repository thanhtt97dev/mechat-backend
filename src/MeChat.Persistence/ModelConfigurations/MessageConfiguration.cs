using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace MeChat.Persistence.ModelConfigurations;
public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable(nameof(Message));

        #region Main properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId);
        builder.Property(x => x.ConversationId);
        builder.Property(x => x.Content);
        builder.Property(x => x.Type);
        #endregion

        #region Audit properties
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        #endregion

        #region Contrains
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Conversation)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}
