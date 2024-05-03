using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.QueryParameters;
using ReverseAnalytics.Infrastructure.Extensions;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity>(ApplicationDbContext context) : IRepositoryBase<TEntity> where TEntity : BaseAuditableEntity
{
    protected readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IEnumerable<TEntity>> FindAllAsync()
    {
        var entities = await _context.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();

        return entities;
    }

    public virtual async Task<PaginatedList<TEntity>> FindAllAsync(PaginatedQueryParameters queryParameters)
    {
        ArgumentNullException.ThrowIfNull(queryParameters);

        var entities = await _context.Set<TEntity>()
            .AsNoTracking()
            .ToPaginatedListAsync(queryParameters.PageNumber, queryParameters.PageSize);

        return entities;
    }

    public async Task<TEntity?> FindByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var createdEntity = await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return createdEntity.Entity;
    }

    public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
        {
            return [];
        }

        foreach (var entity in entities)
        {
            var attachedEntity = _context.Set<TEntity>().Attach(entity);
            attachedEntity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        await _context.SaveChangesAsync();

        return entities;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (!await EntityExistsAsync(entity.Id))
        {
            throw new EntityNotFoundException($"{nameof(TEntity)} with id: {entity.Id} does not exist.");
        }

        var updatedEntity = _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();

        return updatedEntity.Entity;
    }

    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        if (!entities.Any())
        {
            return;
        }

        _context.Set<TEntity>().UpdateRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entityToDelete = await FindByIdAsync(id);

        if (entityToDelete is null)
        {
            throw new EntityNotFoundException($"Entity {typeof(TEntity)} with id: {id} does not exist.");
        }

        _context.Set<TEntity>().Remove(entityToDelete);
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
        => await _context.Set<TEntity>().AnyAsync(x => x.Id == id);

    public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
}
