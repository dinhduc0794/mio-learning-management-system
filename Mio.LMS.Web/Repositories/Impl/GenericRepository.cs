using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Mio.LMS.Web.Models;

namespace Mio.LMS.Web.Repositories.Impl;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly LMSDbContext _context;

    public GenericRepository(LMSDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        var query = _context.Set<T>().AsQueryable();
        if (typeof(BaseModel).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => EF.Property<bool>(e, "IsActive") == true && EF.Property<bool>(e, "IsDeleted") == false);
        }
        return await query.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var query = _context.Set<T>().AsQueryable();
        if (typeof(BaseModel).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => EF.Property<bool>(e, "IsActive") == true && EF.Property<bool>(e, "IsDeleted") == false);
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        var query = _context.Set<T>().AsQueryable();
        if (typeof(BaseModel).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => EF.Property<bool>(e, "IsActive") == true && EF.Property<bool>(e, "IsDeleted") == false);
        }
        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public void Remove(T entity)
    {
        if (entity is BaseModel baseModel)
        {
            baseModel.IsDeleted = true;
            baseModel.IsActive = false;
            baseModel.UpdatedAt = DateTime.Now;
            _context.Set<T>().Update(entity);
        }
        else
        {
            _context.Set<T>().Remove(entity);
        }
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is BaseModel baseModel)
            {
                baseModel.IsDeleted = true;
                baseModel.IsActive = false;
                baseModel.UpdatedAt = DateTime.Now;
                _context.Set<T>().Update(entity);
            }
            else
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }
}