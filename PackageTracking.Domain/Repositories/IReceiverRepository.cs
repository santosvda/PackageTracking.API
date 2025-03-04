using PackageTracking.Domain.Entities;

namespace PackageTracking.Domain.Repositories;
public interface IReceiverRepository
{
    Task<IEnumerable<Receiver>> GetAllAsync();
    Task<Receiver?> GetByIdAsync(int id);
    Task<int> CreateAsync(Receiver entity);
    Task DeleteAsync(Receiver entity);
    Task SaveChangesAsync(Receiver entity);
}
