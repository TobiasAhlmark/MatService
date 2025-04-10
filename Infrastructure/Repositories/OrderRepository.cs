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
        return await _db.Orders
                    .Include(c => c.Customer)
                    .Include(c => c.Restaurant)
                    .Include(c => c.OrderItems)
                    .ThenInclude(c => c.MenuItem)
                    .Include(c => c.Courier)
                    .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _db.Orders
            .Include(o => o.Courier)
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new InvalidOperationException($"Order with ID {id} not found.");
    }

    public async Task<List<Order>> GetByStatus(Order.OrderStatus status, int restaurantId)
    {
        return await _db.Orders
        .Include(o => o.Restaurant)
        .Where(o => o.Status == status && o.RestaurantId == restaurantId)
        .ToListAsync();
    }

    public Task UpdateAsync(Order entity)
    {
        throw new NotImplementedException();
    }
    public Task<bool> AnyAsync(Expression<Func<Order, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> UpdateOrderStatusWithApi(int orderId)
    {
        var order = await GetByIdAsync(orderId);
        order.Status = await GetNextStatus(order.Status);

        await _db.SaveChangesAsync();
        return order;
    }

    public async Task UpdateOrderStatus(Order order)
    {

        order.Status = await GetNextStatus(order.Status);

        await _db.SaveChangesAsync();

    }

    public async Task<Order.OrderStatus> GetNextStatus(Order.OrderStatus currentStatus)
    {
        int nextValue = (int)currentStatus + 1;

        if (nextValue > (int)Order.OrderStatus.Delivered)
        {
            nextValue = (int)Order.OrderStatus.Delivered;
        }
        await _db.SaveChangesAsync();

        return (Order.OrderStatus)nextValue;
    }

    public async Task<Order> SetOrderToCourier(int orderId, int courierId)
    {
        var order = await GetByIdAsync(orderId);
        
        if (order == null)
        {
            throw new KeyNotFoundException("Order not found.");
        }

        if (order.CourierId != null)
        {
            throw new InvalidOperationException("A courier is already assigned to this order.");
        }

        if (order.Status != Order.OrderStatus.Confirmed)
        {
            throw new InvalidOperationException("Order is not in a confirmed state and cannot be assigned a courier.");
        }

        order.CourierId = courierId;
        await _db.SaveChangesAsync();

        return order;
    }

    public async Task<Order> SetOrderStatus (int orderId, Order.OrderStatus status)
    {
        var order = await GetByIdAsync(orderId);
        if(order.Status == status)
        {
            return order;
        }
        else
        {
            order.Status = status;
        }
        await _db.SaveChangesAsync();
        return order;
    }

}