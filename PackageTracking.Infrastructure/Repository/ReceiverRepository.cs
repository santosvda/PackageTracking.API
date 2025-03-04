using Microsoft.EntityFrameworkCore;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Persistence;

namespace PackageTracking.Infrastructure.Repository;
class ReceiverRepository(PackageTrackingDbContext dbContext) : IReceiverRepository
{
    public async Task<IEnumerable<Receiver>> GetAllAsync()
    {
        return await dbContext.Receivers.ToListAsync();
    }

    public async Task<Receiver?> GetById(int id)
    {
        return await dbContext.Receivers
            .Include(r => r.Packages)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<int> Insert(Receiver entity)
    {
        dbContext.Receivers.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }
}
