using MeChat.Common.Constants;
using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class SocialConfiguration : IEntityTypeConfiguration<Social>
{
    public void Configure(EntityTypeBuilder<Social> builder)
    {
        builder.ToTable(nameof(Social));

        #region Main properties
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(seed: 1, increment: 1);
        builder.Property(x => x.Name).IsRequired();
        #endregion

        #region Initial data
        builder.HasData(new Social[]
        {
            new Social {Id = AppConstants.Social.Google, Name = "Google"},
            new Social {Id = AppConstants.Social.Facebook, Name = "Facebook"},
            new Social {Id = AppConstants.Social.Git, Name = "Git"},
        });
        #endregion

    }
}
