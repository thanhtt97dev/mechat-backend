using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class UserSocialConfigutaion : IEntityTypeConfiguration<UserSocial>
{
    public void Configure(EntityTypeBuilder<UserSocial> builder)
    {
        builder.ToTable(nameof(UserSocial));

        builder.HasKey(x => new { x.UserId, x.SocialId});

        builder.Property(x => x.AccountSocialId);
        builder.Property(x => x.DateCreated);
        builder.Property(x => x.DateUpdated);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserSocials)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Social)
            .WithMany(x => x.UserSocials)
            .HasForeignKey(x => x.SocialId);
    }
}
