

using Microsoft.EntityFrameworkCore;

namespace MotrSwap.Models;

public class Repository<T> : IRepository<T> where T : class
{
    protected ApplicationDbContext _context { get; set; }
    private DbSet<T> _dbSet { get; set; }

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public Task<T> GetByIdAsync(int id, QueryOptions<T> options)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}