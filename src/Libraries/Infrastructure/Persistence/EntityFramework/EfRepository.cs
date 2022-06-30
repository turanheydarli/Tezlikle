using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EntityFramework;

public class EfRepository<T>:IRepository<T> where T: BaseEntity
{

    private readonly DbContext _context;
    private readonly DbSet<T> _entities;
    
    public EfRepository(DbContext context)
    {
        _entities = context.Set<T>();
        _context = context;
    }

    protected virtual DbSet<T> Entities => _entities ?? _context.Set<T>();

    public IQueryable<T> GetAll => Entities;
    public IQueryable<T> GetAllNoTracking => Entities.AsNoTracking();
    
    public T GetById(int id)
    {
        return Entities.FirstOrDefault(e => e.Id == id);
    }
    
    public T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Entities.Where(expression);

        includes.Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return query.FirstOrDefault();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Entities.Where(expression);

        includes.Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync();
    }

    public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = Entities.Where(expression);

        includes.Aggregate(query, (current, includeProperty)
            => current.Include(includeProperty));

        return query;
    }

    public T InsertWithoutCommit(T entity)
    { 
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        Entities.Add(entity);
        return entity;
    }

    public async Task<T> InsertAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await Entities.AddAsync(entity);
        await CommitAsync();
        
        return entity;
    }

    public T Insert(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Add(entity);
        Commit();
        
        return entity;
    }

    public int InsertBulk(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));
        
        Entities.AddRange(entities);

        return Commit();
    }

    public int Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        try
        {
            Entities.Update(entity);
            Commit();
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    public async Task<int> UpdateAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        
        try
        {
            Entities.Update(entity);
            await CommitAsync();
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    public int UpdateWithoutCommit(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        try
        {
            Entities.Update(entity);
            return 1;
        }
        catch
        {
            return -1;
        }
    }

    public int Commit()
    {
        try
        {
            _context.SaveChanges();
            return 1;
        }
        catch (DbUpdateException e)
        {
            GetFullErrorTextAndRollbackEntityChanges(e);
            return -1;
        }
    }

    public async Task<int> CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return 1;
        }
        catch (DbUpdateException e)
        {
            GetFullErrorTextAndRollbackEntityChanges(e);
            return -1;
        }
    }
    
    public int Delete(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
        return Commit();
    }

    public async Task<int> DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        await Task.Run(() => Entities.Remove(entity));

        return await CommitAsync();
    }

    public void DeleteWithoutCommit(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
    }
    
    public int DeleteBulk(IEnumerable<T> entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        Entities.RemoveRange(entities);
        return Commit();
    }

    public bool Any(Expression<Func<T, bool>> expression)
    {
        return Entities.Any(expression);
    }

    #region Utilities
    protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
    {
        if (_context is DbContext dbContext)
        {
            var entries = dbContext.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

            entries.ForEach(entry =>
            {
                try
                {
                    entry.State = EntityState.Unchanged;
                }
                catch (InvalidOperationException)
                {
                }
            });
        }

        try
        {
            _context.SaveChanges();
            return exception.ToString();
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
    #endregion
}