using System.Data;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using Domain.Common;

namespace Infrastructure.Persistence.Dapper;

public class DapperRepository<T>:IRepository<T> where T : BaseEntity
{
    private IDbConnection _db;
    
    public T GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll { get; }
    public IQueryable<T> GetAllNoTracking { get; }
    
    public T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetMany(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
    {
        throw new NotImplementedException();
    }

    public T InsertWithoutCommit(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> InsertAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public T Insert(T entity)
    {
        throw new NotImplementedException();
    }

    public int InsertBulk(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public int Update(T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public int UpdateWithoutCommit(T entity)
    {
        throw new NotImplementedException();
    }

    public int Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteWithoutCommit(T entity)
    {
        throw new NotImplementedException();
    }

    public int Commit()
    {
        throw new NotImplementedException();
    }

    public async Task<int> CommitAsync()
    {
        throw new NotImplementedException();
    }

    public int DeleteBulk(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }

    public bool Any(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }
}