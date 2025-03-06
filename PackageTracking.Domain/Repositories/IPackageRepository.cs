using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;

namespace PackageTracking.Domain.Repositories;
public interface IPackageRepository
{
    Task<IEnumerable<Package>> GetAllAsync();
    Task<(IEnumerable<Package>, int)> GetAllMatchingAsync(string? searchDescription, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection);
    Task<Package?> GetByIdAsync(int receiverId, int id);
    Task<int> CreateAsync(Package entity);
    Task DeleteAsync(Package entity);
    Task SaveChanges();
}
