using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MotrSwap.Models;


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehiclePosting> VehiclePostings { get; set; }
    public DbSet<VehicleImage> VehicleImages { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        base.OnModelCreating((builder));
        
        builder.Entity<VehiclePosting>()
            .HasOne(vp => vp.User)
            .WithMany(u => u.VehiclePostings)
            .HasForeignKey(vp => vp.UserId);
        
        builder.Entity<VehiclePosting>()
            .HasOne(vp => vp.Vehicle)
            .WithMany(v => v.VehiclePostings)
            .HasForeignKey(vp => vp.VehicleId);
        
        builder.Entity<VehicleImage>()
            .HasOne(img => img.VehiclePosting)
            .WithMany(vp => vp.VehicleImages)
            .HasForeignKey(img => img.VehiclePostingId);
    }
}
