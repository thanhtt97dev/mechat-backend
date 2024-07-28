using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class UserSocialConfigutaion : IEntityTypeConfiguration<UserSocial>
{
    public void Configure(EntityTypeBuilder<UserSocial> builder)
    {
        builder.ToTable(nameof(UserSocial));
        #region Main properties
        builder.HasKey(x => new { x.UserId, x.SocialId});

        builder.Property(x => x.AccountSocialId);
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        #endregion

        #region Audit properties
        builder.Property(x => x.CreatedBy);
        builder.Property(x => x.ModifiedBy);
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        builder.Property(x => x.DeleteAt);
        builder.Property(x => x.IsDeleted);

        builder.HasQueryFilter(x => x.IsDeleted == false);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.UserSocials)
            .HasForeignKey(x => x.UserId);
        builder
            .HasOne(x => x.Social)
            .WithMany(x => x.UserSocials)
            .HasForeignKey(x => x.SocialId);
        #endregion

    }
}
