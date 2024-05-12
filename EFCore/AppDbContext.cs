using bk_backend.Model;
using Microsoft.EntityFrameworkCore;

namespace bk_backend.EFCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserCv> UserCvs { get; set; }
    public DbSet<Enterprise> Enterprises { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<CompanyCv> CompanyCvs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("T_User");
        modelBuilder.Entity<UserCv>().ToTable("T_UserCv");
        modelBuilder.Entity<Enterprise>().ToTable("T_Enterprise");
        modelBuilder.Entity<Job>().ToTable("T_Job");
        modelBuilder.Entity<CompanyCv>().ToTable("T_CompanyCv");
        
    }

   
}