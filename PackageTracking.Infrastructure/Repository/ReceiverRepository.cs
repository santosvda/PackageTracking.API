using Microsoft.EntityFrameworkCore;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Persistence;

namespace PackageTracking.Infrastructure.Repository;
class ReceiverRepository(PackageTrackingDbContext dbContext) : IReceiverRepository
{
    public async Task<IEnumerable<Receiver>> GetAllAsync()
    {
        return await dbContext.Receivers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Receiver?> GetByIdAsync(int id)
    {
        return await dbContext.Receivers
            .Include(r => r.Packages)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<int> CreateAsync(Receiver entity)
    {
        dbContext.Receivers.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(Receiver entity)
    {
        dbContext.Remove(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task SaveChangesAsync(Receiver entity)
    {
        dbContext.Update(entity);

        await dbContext.SaveChangesAsync();
    }
}
