using MeChat.Common.Constants;
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
            new(){Id = 1, Name = "Waiting accept"},
            new(){Id = 2, Name = "Accepted"},
            new(){Id = 3, Name = "Block"},
            //Delete record if unfriend
        });
        #endregion
    }
}
