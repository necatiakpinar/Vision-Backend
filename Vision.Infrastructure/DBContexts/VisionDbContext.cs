using Microsoft.EntityFrameworkCore;
using Vision.Domain.Entities;
using Vision.Domain.Identity;

namespace Vision.Infrastructure.DBContexts;

public class VisionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<AppRole> Roles { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Leaderboard> Leaderboards { get; set; }
    public DbSet<ImageModel> ProfileImages { get; set; }
    public DbSet<FileModel> UserMediaFiles { get; set; }
    public DbSet<SharedFilesModel> SharedFiles { get; set; }
    public DbSet<OwnedFilesModel?> OwnedFiles { get; set; }
    public DbSet<MentorModel> Mentors { get; set; }
    //public DbSet<SubscriberModel> Subscribers { get; set; }

    public VisionDbContext(DbContextOptions<VisionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>();
        modelBuilder.Entity<UserProfile>();
        modelBuilder.Entity<AppRole>();
        modelBuilder.Entity<Leaderboard>();
        modelBuilder.Entity<ImageModel>();
        modelBuilder.Entity<FileModel>();
        modelBuilder.Entity<SharedFilesModel>();
        modelBuilder.Entity<OwnedFilesModel>();
        modelBuilder.Entity<MentorModel>();
        //modelBuilder.Entity<SubscriberModel>();

    }
    
}