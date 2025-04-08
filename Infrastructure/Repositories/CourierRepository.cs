using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;

namespace FoodOnDelivery.Infrastructure.Repositories;

public class CourierRepository : IRepo<Courier>
{
    private readonly AppDbContext _db;

    public CourierRepository(AppDbContext appDb)
    {
        _db = appDb;
    }
    public async Task AddAsync(Courier entity)
    {
        await _db.Couriers.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<Courier, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Courier entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Courier>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Courier> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Courier entity)
    {
        throw new NotImplementedException();
    }
}