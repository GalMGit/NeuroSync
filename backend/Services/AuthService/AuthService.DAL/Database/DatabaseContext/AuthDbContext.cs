using AuthService.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DAL.Database.DatabaseContext;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}