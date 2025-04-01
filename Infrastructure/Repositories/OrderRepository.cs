using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;


namespace FoodOnDelivery.Infrastructure.Repositories;

public class OrderRepository : IRepo<Order>
{
    private readonly AppDbContext _db;

    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(Order entity)
    {
        await _db.Orders.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order entity)
    {
        _db.Orders.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _db.Orders.ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id) 
            ?? throw new InvalidOperationException($"Order with ID {id} not found.");
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
    public Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate)
    {
        throw new NotImplementedException();
    }
   
   
}