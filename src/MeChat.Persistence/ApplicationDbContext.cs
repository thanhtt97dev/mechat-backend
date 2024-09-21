using MeChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeChat.Persistence;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Social> Socials { get; set; }  
    public DbSet<UserSocial> UserSocials { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<FriendStatus> FriendStatus { get; set; }
    public DbSet<Notification> Notifications { get; set; }
}
