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
                Avatar = "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld.jpg",
                CoverPhoto = "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                ModifiledDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            },
            new User(){
                Id = Guid.Parse("A09C6CF6-710E-466F-E716-08DCD4F11F19"),
                Username = "test1",
                Password = "test1",
                Fullname = "test1",
                RoleId = AppConstants.Role.User,
                Email = "leduchieu2001x@gmail.com",
                Avatar = "https://me-chat.s3.ap-southeast-1.amazonaws.com/hieuld02.jpg",
                CoverPhoto = "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                ModifiledDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            },
            new User(){
                Id = Guid.Parse("6b44f7b1-b873-44ef-9491-ffe41f5775ed"),
                Username = "test2",
                Password = "test2",
                Fullname = "test2",
                RoleId = AppConstants.Role.User,
                Email = "hieuldhe150703@fpt.edu.vn",
                Avatar = "https://me-chat.s3.ap-southeast-1.amazonaws.com/thanhtt.jpg",
                CoverPhoto = "https://me-chat.s3.ap-southeast-1.amazonaws.com/coverphoto.jpg",
                Status = AppConstants.User.Status.Activate,
                CreatedDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                ModifiledDate = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            }
        });
        #endregion

    }
}
