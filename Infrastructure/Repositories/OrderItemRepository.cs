using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace FoodOnDelivery.Infrastructure.Repositories;

public class OrderItemRepository : IRepo<OrderItem>
{
    private readonly AppDbContext _db;

    public OrderItemRepository (AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(OrderItem entity)
    {
        await _db.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task AddListAsync(List<OrderItem> entity)
    {
        await _db.AddRangeAsync(entity);
        await _db.SaveChangesAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<OrderItem, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderItem entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderItem> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(OrderItem entity)
    {
        throw new NotImplementedException();
    }
}
