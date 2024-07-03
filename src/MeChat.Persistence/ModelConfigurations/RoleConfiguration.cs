using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeChat.Persistence.ModelConfigurations;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn(seed: 1, increment: 1);

        builder.Property(x => x.RoleName).HasMaxLength(100).IsRequired(true);
        builder.Property(x => x.DateCreated);
        builder.Property(x => x.DateUpdated);

        builder.HasData(new Role[]
        {
            new Role { Id = 1, RoleName = "Admin", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
            new Role { Id = 2, RoleName = "User", DateCreated = DateTime.Now, DateUpdated = DateTime.Now },
        });
    }
}
