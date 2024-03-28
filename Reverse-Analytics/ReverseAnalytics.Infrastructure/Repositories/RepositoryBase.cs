using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public abstract class RepositoryBase<T>(ApplicationDbContext context) : IRepositoryBase<T> where T : BaseAuditableEntity
{
    protected readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<T>> FindAllAsync()
    {
        var entities = await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        return entities;
    }

    public virtual async Task<PaginatedList<T>> FindAllAsync(PaginatedQueryParameters queryParameters)
    {
        var entities = await _context.Set<T>()
            .AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return entities;
    }

    public async Task<T> FindByIdAsync(int id)
    {
        var entity = await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            throw new EntityNotFoundException($"Entity {typeof(T)} with id: {id} does not exist.");
        }

        return entity;
    }

    public async Task<T> CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var createdEntity = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }

    public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
        {
            return Enumerable.Empty<T>();
        }

        foreach (var entity in entities)
        {
            var attachedEntity = _context.Set<T>().Attach(entity);
            attachedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        await _context.SaveChangesAsync();

        return entities;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var updatedEntity = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();

        return updatedEntity.Entity;
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
        {
            return;
        }

        _context.Set<T>().UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToDelete = await FindByIdAsync(id);

        if (entityToDelete is null)
        {
            throw new EntityNotFoundException($"Entity {typeof(T)} with id: {id} does not exist.");
        }

        _context.Set<T>().Remove(entityToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<int> ids)
    {
        ArgumentNullException.ThrowIfNull(ids);

        if (!ids.Any())
        {
            return;
        }

        foreach (var id in ids)
        {
            await DeleteAsync(id);
        }
    }

    public async Task<bool> EntityExistsAsync(int id)
        => await _context.Set<T>().AnyAsync(x => x.Id == id);

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
