using MeChat.Common.Shared.Constants;
using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class FriendStatusConfiguration : IEntityTypeConfiguration<FriendStatus>
{
    public void Configure(EntityTypeBuilder<FriendStatus> builder)
    {
        builder.ToTable(nameof(FriendStatus));

        #region Main properties
        builder.HasKey(x => new { x.Id });
        #endregion

        #region Initial data
        builder.HasData(new FriendStatus[]
        {
            new(){Id = 1, Name = "UnFriend"},
            new(){Id = 2, Name = "Waiting accept"},
            new(){Id = 3, Name = "Accepted"},
            new(){Id = 4, Name = "Block"},
        });
        #endregion
    }
}
