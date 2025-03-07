﻿using AdvicerApp.Core.Entities.Common;
using AdvicerApp.Core.Repositories;
using AdvicerApp.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdvicerApp.DAL.Repositories;

public class GenericRepository<T>(AdvicerAppDbContext _context) : IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table => _context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task DeleteAndSaveAsync(int id)
    {
        await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        Table.Remove(entity!);
    }

    public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table;
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).ToListAsync();
    }

    public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select, bool isDeleted = false)
    {
        IQueryable<T> query = _context.Set<T>();
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        return await query.Select(select).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table;
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }

        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }

        return await query.Select(select).ToListAsync();
    }

    public async Task<U?> GetByIdAsync<U>(int id, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(x => x.Id == id);
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }

        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();
    }

    public async Task<T?> GetByIdAsync(int id, Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(x => x.Id == id);
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }

        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();

    }

    public async Task<U?> GetFirstAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(expression);
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();
    }

    public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(expression);
        if (isDeleted)
        {
            query = query.Where(x => x.IsDeleted);
        }

        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();
    }

    public IQueryable<U> GetQuery<U>(Expression<Func<T, U>> select, bool asNoTracking = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table;
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        return query.Select(select);

    }

    public async Task<IEnumerable<U>> GetWhereAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(expression);
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).ToListAsync();

    }

    public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false)
    {
        IQueryable<T> query = Table.Where(expression);
        if (!isDeleted)
        {
            query = query.Where(x => x.IsDeleted == false);
        }
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).ToListAsync();
    }

    public Task<bool> IsExistAsync(int id)
        => Table.AnyAsync(x => x.Id == id);

    public Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => Table.AnyAsync(expression);

    public void ReverseSoftDelete(T entity)
    {
        entity.IsDeleted = false;
    }

    public async Task ReverseSoftDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        entity!.IsDeleted = false;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        entity!.IsDeleted = true;
    }
}

