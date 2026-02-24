using CommunityService.CORE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.DAL.Database.DatabaseContext;

public class CommunityDbContext : DbContext
{
    public CommunityDbContext(DbContextOptions<CommunityDbContext> options) : base(options) {}
    
    public DbSet<Community> Communities { get; set; }
    public DbSet<CommunityMember> CommunityMembers { get; set; }
}