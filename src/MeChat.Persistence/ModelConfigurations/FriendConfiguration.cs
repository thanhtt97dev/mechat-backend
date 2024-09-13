using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class FriendConfiguration : IEntityTypeConfiguration<Friend>
{
    public void Configure(EntityTypeBuilder<Friend> builder)
    {
        builder.ToTable(nameof(Friend));

        #region Main property
        builder.HasKey(x => new { x.UserFirstId, x.UserSecondId });
        builder.Property(x => x.SpecifierId);
        builder.Property(x => x.Status);
        #endregion

        #region Audit property
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        #endregion

        #region Contrains
        builder.HasOne(friend => friend.UserFirst)
            .WithMany(user => user.Friends)
            .HasForeignKey(friend => friend.UserFirstId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(friend => friend.UserSecond)
            .WithMany()
            .HasForeignKey(friend => friend.UserSecondId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(friend => friend.Specifier)
                .WithMany()
                .HasForeignKey(friend => friend.SpecifierId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(friend => friend.FriendStatus)
                .WithMany(friendStatus => friendStatus.Friends)
                .HasForeignKey(friend => friend.Status);
        #endregion
    }
}
