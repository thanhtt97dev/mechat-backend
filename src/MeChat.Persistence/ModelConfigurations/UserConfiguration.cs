using MeChat.Common.Shared.Constants;
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
        builder.Property(x => x.CoverPhoto);
        #endregion

        #region Audit properties
        builder.Property(x => x.CreatedDate);
        builder.Property(x => x.ModifiledDate);
        #endregion

        #region Constraints
        builder
            .HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .IsRequired();
        #endregion

        #region Initial data
        builder.HasData(new User[]
        {
            new User(){
                Id = Guid.Parse("ED003C55-0557-4885-9055-C0C47CC4F7AB"),
                Username = "test",
                Password = "test",
                Fullname = "test",
                RoleId = AppConstants.Role.User,
                Email = "mechat.mail@gmail.com",
                Avatar = "https://scontent.fhan2-4.fna.fbcdn.net/v/t39.30808-6/458141307_3752163308376305_7786396549520709717_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=mZeMLHtxPS4Q7kNvgHz6M_O&_nc_ht=scontent.fhan2-4.fna&_nc_gid=AN95bCnQULrHiAXmIyVA2wZ&oh=00_AYC4yBc8ZZkdyQboPp8z4T0mdBHyc6PRqssrdLH_ahIDfQ&oe=66EC3E3A",
                CoverPhoto = "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            },
            new User(){
                Id = Guid.Parse("A09C6CF6-710E-466F-E716-08DCD4F11F19"),
                Username = "test1",
                Password = "test1",
                Fullname = "test1",
                RoleId = AppConstants.Role.User,
                Email = "leduchieu2001x@gmail.com",
                Avatar = "https://scontent.fhan2-3.fna.fbcdn.net/v/t39.30808-1/376711391_3582728322054427_6580417315416639743_n.jpg?stp=dst-jpg_s200x200&_nc_cat=111&ccb=1-7&_nc_sid=0ecb9b&_nc_ohc=DBrY6E9OFCkQ7kNvgFyeI5-&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A3dFMcRG-YVhj7w0WRNg1wT&oh=00_AYANj8xO-MOrS4l1FZE9wJycAe0ibFNv9ZGaextYUZegDg&oe=66EC242D",
                CoverPhoto = "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            },
            new User(){
                Id = Guid.Parse("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                Username = "test2",
                Password = "test2",
                Fullname = "test2",
                RoleId = AppConstants.Role.User,
                Email = "hieuldhe150703@fpt.edu.vn",
                Avatar = "https://scontent.fhan2-3.fna.fbcdn.net/v/t1.6435-9/51863877_2216876628639610_6964562136462786560_n.jpg?_nc_cat=108&ccb=1-7&_nc_sid=13d280&_nc_ohc=CkFWK4i94xMQ7kNvgFKmft2&_nc_ht=scontent.fhan2-3.fna&_nc_gid=A6Zx5iuo9_o4-xw0tzRnM3L&oh=00_AYC9tQUQYhvdGa3-WzEBShl9D25MJbY_dPfAJx_vDHwlHA&oe=670DCD49",
                CoverPhoto = "https://images.pexels.com/photos/956981/milky-way-starry-sky-night-sky-star-956981.jpeg?auto=compress&cs=tinysrgb&w=600",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = DateTimeOffset.Now,
                ModifiledDate = DateTimeOffset.Now,
            }
        });
        #endregion

    }
}
