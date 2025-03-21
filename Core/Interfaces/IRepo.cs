using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.Core.Interfaces;

public interface IRepo<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    // Andra metoder om du vill
}
