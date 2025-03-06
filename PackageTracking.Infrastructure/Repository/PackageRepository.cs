using Microsoft.EntityFrameworkCore;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Persistence;

namespace PackageTracking.Infrastructure.Repository;
class PackageRepository(PackageTrackingDbContext dbContext) : IPackageRepository
{
    public async Task<IEnumerable<Package>> GetAllAsync()
    {
        return await dbContext.Packages
            .Include(r => r.Statuses)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Package?> GetByIdAsync(int receiverId, int id)
    {
        return await dbContext.Packages
            .Include(r => r.Statuses)
            .FirstOrDefaultAsync(r => r.Id == id && r.ReceiverId == receiverId);
    }

    public async Task<int> CreateAsync(Package entity)
    {
        dbContext.Packages.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(Package entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public Task SaveChanges()
     => dbContext.SaveChangesAsync();
}
