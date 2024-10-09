using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class ConversationConfiguration : IEntityTypeConfiguration<Conversation>
{
    public void Configure(EntityTypeBuilder<Conversation> builder)
    {
        builder.ToTable(nameof(Conversation));

        #region Main properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name);
        builder.Property(x => x.Avatar);
        builder.Property(x => x.Type);
        builder.Property(x => x.AdministratorId);
        #endregion

        #region Audit properties
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.ModifiedBy);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.Administrator)
            .WithMany(x => x.Conversations)
            .HasForeignKey(x => x.AdministratorId)
            .IsRequired();
        #endregion
    }
}
