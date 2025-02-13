using System.Linq.Expressions;

namespace AdvicerApp.Core.Repositories;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select,bool asNoTrack = true, bool isDeleted = false);
    Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select, bool isDeleted = false);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false);

    Task<U?> GetByIdAsync<U>(int id, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false);
    Task<T?> GetByIdAsync(int id,Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false);
    //Task<U?> GetByIdAsync<U>(int id, Expression<Func<T, U>> select, bool isDeleted = false);

    Task<IEnumerable<U>> GetWhereAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false);
    Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> expression, Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false);

    //Task<IEnumerable<U>> GetWhereAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool isDeleted = false);

    Task<U?> GetFirstAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = false);
    Task<T?> GetFirstAsync(Expression<Func<T, bool>> expression, Expression<Func<T, T>> select, bool asNoTrack = true, bool isDeleted = false);
    //Task<U?> GetFirstAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select,bool isDeleted = false);

    IQueryable<U> GetQuery<U>(Expression<Func<T, U>> select, bool asNoTracking = true,bool isDeleted = false);
    Task<bool> IsExistAsync(int id);
    Task<bool> IsExistAsync(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);
    void Delete(T entity);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    void SoftDelete(T entity);
    Task ReverseSoftDeleteAsync(int id);
    void ReverseSoftDelete(T entity);
    Task DeleteAndSaveAsync(int id);
    Task<int> SaveAsync();
}
