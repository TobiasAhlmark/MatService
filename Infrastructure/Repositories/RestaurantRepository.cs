using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;


namespace FoodOnDelivery.Infrastructure.Repositories;

public class RestaurantRepository : IRepo<Restaurant>
{
    private readonly AppDbContext _db;

    public RestaurantRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Restaurant entity)
    {
        await _db.Restaurants.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<Restaurant, bool>> predicate)
    {
        return await _db.Restaurants.AnyAsync(predicate);
    }

    public async Task DeleteAsync(Restaurant entity)
    {
        _db.Restaurants.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Restaurant>> GetAllAsync()
    {
        return await _db.Restaurants.Include(m => m.Menu).ThenInclude(i => i.Items).ToListAsync();
    }

    public async Task<Restaurant> GetByIdAsync(int id)
    {
        return await _db.Restaurants
        .Include(r => r.Menu)
        .ThenInclude(r => r.Items)
        .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task UpdateAsync(Restaurant entity)
    {
        _db.Restaurants.Update(entity);
        await _db.SaveChangesAsync();
    }
}