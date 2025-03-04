using PackageTracking.Domain.Entities;

namespace PackageTracking.Domain.Repositories;
public interface IReceiverRepository
{
    Task<IEnumerable<Receiver>> GetAllAsync();
    Task<Receiver?> GetById(int id);
    Task<int> Insert(Receiver entity);
}
