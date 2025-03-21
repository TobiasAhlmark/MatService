using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace FoodOnDelivery.Infrastructure.Repositories;

public class ItemRepository : IRepo<MenuItem>
{
    private readonly AppDbContext _db;

    public ItemRepository(AppDbContext appDbContext)
    {
        _db = appDbContext;
    }
    public async Task AddAsync(MenuItem entity)
    {
        await _db.MenuItems.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(MenuItem entity)
    {
        _db.MenuItems.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<MenuItem>> GetAllAsync()
    {
        return await _db.MenuItems.ToListAsync();
    }

    public async Task<MenuItem> GetByIdAsync(int id)
    {
        return await _db.MenuItems.FirstOrDefaultAsync(m => m.Id == id);
    }

    public Task UpdateAsync(MenuItem entity)
    {
        throw new NotImplementedException();
    }
     public Task<bool> AnyAsync(Expression<Func<MenuItem, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}