using Microsoft.EntityFrameworkCore;
using PackageTracking.Domain.Constants;
using PackageTracking.Domain.Entities;
using PackageTracking.Domain.Repositories;
using PackageTracking.Infrastructure.Persistence;
using System.Linq.Expressions;

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

    public async Task<(IEnumerable<Package>, int)> GetAllMatchingAsync(string? searchDescription, int pageNumber, int pageSize, string? sortBy, SortDirection sortDirection)
    {
        var search = searchDescription?.ToLower();

        var baseQuery = dbContext.Packages
            .Include(r => r.Statuses)
            .Where(r => search == null
                || r.Description.ToLower().Contains(search)
                || r.Adress.Street.ToLower().Contains(search));

        var totalCount = await baseQuery.CountAsync();

        if (sortBy is not null)
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Package, object>>>
            {
                { nameof(Package.Description), r => r.Description },
                { nameof(Package.Adress.Street), r => r.Adress.Street },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Asc
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var packages = await baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (packages, totalCount);
    }


}
