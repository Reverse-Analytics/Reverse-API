using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.QueryParameters;

namespace ReverseAnalytics.Domain.Interfaces.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> FindAllAsync();

    Task<PaginatedList<TEntity>> FindAllAsync(PaginatedQueryParameters queryParameters);

    Task<TEntity?> FindByIdAsync(int id);

    Task<TEntity> CreateAsync(TEntity entity);

    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities);

    Task DeleteAsync(int id);

    Task DeleteRangeAsync(IEnumerable<int> ids);

    Task<int> SaveChangesAsync();

    Task<bool> EntityExistsAsync(int id);
}
