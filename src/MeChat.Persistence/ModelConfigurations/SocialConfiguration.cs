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
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);

        builder.HasData(new Social[]
        {
            new Social { Id = SocialConstants.Google, Name = "Google", CreatedDate = DateTime.Now, ModifiledDate = DateTime.Now },
            new Social { Id = SocialConstants.Facebook, Name = "Facebook", CreatedDate = DateTime.Now, ModifiledDate = DateTime.Now },
            new Social { Id = SocialConstants.Git, Name = "Git", CreatedDate = DateTime.Now, ModifiledDate = DateTime.Now },
        });
    }
}
