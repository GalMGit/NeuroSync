using CommentService.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommentService.DAL.Database.DatabaseContext;

public class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) {}
    
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}