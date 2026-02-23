using Microsoft.EntityFrameworkCore;
using PostService.CORE.Entities;

namespace PostService.DAL.Database.DatabaseContext;

public class PostDbContext : DbContext
{
    public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) {}
    
    public DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}