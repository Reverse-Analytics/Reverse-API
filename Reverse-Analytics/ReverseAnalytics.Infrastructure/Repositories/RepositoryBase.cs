﻿using Microsoft.EntityFrameworkCore;
using ReverseAnalytics.Domain.Common;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Infrastructure.Persistence;

namespace ReverseAnalytics.Infrastructure.Repositories
{
    public class RepositoryBase<T>(ApplicationDbContext context) : IRepositoryBase<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public Task<IEnumerable<T>> FindAllAsync(int pageSize = 0, int pageNumber = 0)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Func<T, bool> predicate)
        {
            var entities = await _context.Set<T>()
                .Where(x => predicate.Invoke(x))
                .ToListAsync();

            return entities;
        }

        public async Task<T> FindByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException($"Entity {typeof(T)} with id: {id} does not exist.");
            }

            return entity;
        }

        public async Task<T> Create(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            var createdEntity = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return createdEntity.Entity;
        }

        public async Task<IEnumerable<T>> CreateRange(IEnumerable<T> entities)
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

        public async Task Update(T entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> entities)
        {
            ArgumentNullException.ThrowIfNull(entities);

            if (!entities.Any())
            {
                return;
            }

            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entityToDelete = await FindByIdAsync(id);

            if (entityToDelete is null)
            {
                throw new EntityNotFoundException($"Entity {typeof(T)} with id: {id} does not exist.");
            }

            _context.Set<T>().Remove(entityToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<int> ids)
        {
            ArgumentNullException.ThrowIfNull(ids);

            if (!ids.Any())
            {
                return;
            }

            foreach (var id in ids)
            {
                await Delete(id);
            }
        }

        public Task<bool> EntityExistsAsync(T entity)
            => _context.Set<T>().AnyAsync(e => e.Id == entity.Id);

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
