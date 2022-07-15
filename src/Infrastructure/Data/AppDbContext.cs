using Domain.Entities.Auth;
using Domain.Entities.Core;
using Infrastructure.Data.Configs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AppDbContext : DbContext
{
    public DbSet<AuthLocal> AuthLocal { get; set; }
    public DbSet<AuthProvider> AuthProvider { get; set; }
    public DbSet<AuthSession> AuthSession { get; set; }

    public DbSet<Permission> Permission { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<RolePermission> RolePermission { get; set; }
    public DbSet<RoleUser> RoleUser { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserNotification> UserNotification { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new RoleConfig());
        modelBuilder.ApplyConfiguration(new RoleUserConfig());
        modelBuilder.ApplyConfiguration(new AuthLocalConfig());
    }
}
