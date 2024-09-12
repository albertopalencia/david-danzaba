using Microsoft.EntityFrameworkCore;
using RealState.Domain.Common;
using RealState.Infrastructure.DataSource;
using RealState.Infrastructure.Ports;
using System.Linq.Expressions;

namespace RealState.Infrastructure.Adapters;

public class GenericRepository<T> : IRepository<T> where T : DomainEntity
{
    private readonly DataContext _context;
    private readonly DbSet<T> _dataset;
    private readonly char[] _separator = [','];

    public GenericRepository(DataContext context)
    {
        _context = context;
        _dataset = _context.Set<T>();
    }
     
    public async Task<IEnumerable<T>> GetManyAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeStringProperties = "", bool isTracking = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeStringProperties))
        {
            query = includeStringProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync().ConfigureAwait(false);
        }

        return (!isTracking) ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetManyPagedAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeStringProperties = "",
        bool isTracking = false, int pageNumber = 1, int pageSize = 30)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (!string.IsNullOrEmpty(includeStringProperties))
        {
            query = includeStringProperties.Split(',', StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        if (!isTracking)
        {
            query = query.AsNoTracking();
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }
          
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(); 

        return items;
    }

    public async Task<T> AddAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
        await _dataset.AddAsync(entity);
        return entity;
    }

    public void DeleteAsync(T entity)
    {
        _ = entity ?? throw new ArgumentNullException(nameof(entity), "Entity can not be null");
        _dataset.Remove(entity);
    }

    public async Task<T> GetOneAsync(Guid id, string? includeStringProperties = default)
    {
        var query = _dataset.AsQueryable();

        if (string.IsNullOrEmpty(includeStringProperties))
            return await query.FirstOrDefaultAsync(entity => entity.Id == id) ?? default!;
        query = includeStringProperties.Split(_separator, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(entity => entity.Id == id) ?? default!;
    }

    public void UpdateAsync(T entity)
    {
        _dataset.Update(entity);
    }

    public Task<int> GetCountAsync()
    {
        return _dataset.CountAsync();
    }

}
