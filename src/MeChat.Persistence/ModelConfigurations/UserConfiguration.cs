using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        #region Main Properties
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasMaxLength(50);
        builder.Property(x => x.Username).HasMaxLength(50);
        builder.Property(x => x.Password).HasMaxLength(50);
        builder.Property(x => x.Email);
        builder.Property(x => x.Status);
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
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoldeId)
            .IsRequired();
        #endregion

    }
}
