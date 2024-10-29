using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable(nameof(Notification));

        #region Main properties
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ReceiverId);
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.Type);
        builder.Property(x => x.IsReaded);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.Receiver)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Requester)
            .WithMany()
            .HasForeignKey(x => x.RequesterId)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion
    }
}
