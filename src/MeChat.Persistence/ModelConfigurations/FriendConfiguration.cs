using MeChat.Common.Shared.Constants;
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
        builder.Property(x => x.OldStatus);
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
            .HasForeignKey(friend => friend.Status)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(friend => friend.OldFriendStatus)
            .WithMany()
            .HasForeignKey(friend => friend.OldStatus)
            .OnDelete(DeleteBehavior.Restrict);
        #endregion

        #region Initial data
        builder.HasData(new Friend[]
        {
            new Friend(){ //1-2
                UserFirstId = Guid.Parse("ED003C55-0557-4885-9055-C0C47CC4F7AB"),
                UserSecondId = Guid.Parse("A09C6CF6-710E-466F-E716-08DCD4F11F19"),
                SpecifierId = Guid.Parse("ED003C55-0557-4885-9055-C0C47CC4F7AB"),
                OldStatus = AppConstants.FriendStatus.UnFriend,
                Status = AppConstants.FriendStatus.WatitingAccept,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            },
            new Friend(){ //1-3
                UserFirstId = Guid.Parse("ED003C55-0557-4885-9055-C0C47CC4F7AB"),
                UserSecondId = Guid.Parse("6B44F7B1-B873-44EF-9491-FFE41F5775ED"),
                SpecifierId = Guid.Parse("ED003C55-0557-4885-9055-C0C47CC4F7AB"),
                OldStatus = AppConstants.FriendStatus.WatitingAccept,
                Status = AppConstants.FriendStatus.Accepted,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            },
            new Friend(){ //2-3
                UserFirstId = Guid.Parse("A09C6CF6-710E-466F-E716-08DCD4F11F19"),
                UserSecondId = Guid.Parse("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                SpecifierId = Guid.Parse("A09C6CF6-710E-466F-E716-08DCD4F11F19"),
                OldStatus = AppConstants.FriendStatus.WatitingAccept,
                Status = AppConstants.FriendStatus.Accepted,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            },
        });
        #endregion
    }
}
