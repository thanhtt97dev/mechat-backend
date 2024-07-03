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

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.DateCreated);
        builder.Property(x => x.DateUpdated);

        builder.HasData(new Social[]
        {
            new Social { Id = SocialConstants.Google, Name = "Google", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
            new Social { Id = SocialConstants.Facebook, Name = "Facebook", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
            new Social { Id = SocialConstants.Git, Name = "Git", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
        });
    }
}
