using Microsoft.EntityFrameworkCore;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Infrastructure.Persistence;

internal class PackageTrackingDbContext (DbContextOptions<PackageTrackingDbContext> options) : DbContext(options)
{
    internal DbSet<Package> Packages { get; set; }
    internal DbSet<Receiver> Receivers { get; set; }
    internal DbSet<Status> Statuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Receiver>()
           .HasMany(r => r.Packages)
           .WithOne()
           .HasForeignKey(p => p.ReceiverId)
           .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>(entity =>
        {
            entity.OwnsOne(p => p.Adress);

            entity.HasMany(p => p.Statuses)
                .WithOne()
                .HasForeignKey(s => s.PackageId);
        });
    }
}