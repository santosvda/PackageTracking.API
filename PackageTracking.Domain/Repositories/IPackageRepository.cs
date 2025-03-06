using PackageTracking.Domain.Entities;

namespace PackageTracking.Domain.Repositories;
public interface IPackageRepository
{
    Task<IEnumerable<Package>> GetAllAsync();
    Task<Package?> GetByIdAsync(int receiverId, int id);
    Task<int> CreateAsync(Package entity);
    Task DeleteAsync(Package entity);
    Task SaveChanges();
}
