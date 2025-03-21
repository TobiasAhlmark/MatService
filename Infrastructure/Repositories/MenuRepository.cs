using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace FoodOnDelivery.Infrastructure.Repositories;

public class MenuRepository : IRepo<Menu>
{
    private readonly AppDbContext _db;
    public MenuRepository(AppDbContext appDbContext)
    {
        _db = appDbContext;
    }
    public async Task AddAsync(Menu entity)
    {
        await _db.Menus.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<Menu, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Menu entity)
    {
        _db.Menus.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Menu>> GetAllAsync()
    {
        return await _db.Menus.Include(r => r.Restaurant).ToListAsync();
    }

    public async Task<Menu> GetByIdAsync(int id)
    {
        return await _db.Menus
        .Include(r => r.Items)
        .FirstOrDefaultAsync(m => m.Id == id);
    }

    public Task UpdateAsync(Menu entity)
    {
        throw new NotImplementedException();
    }

    public async Task AddItemsToMenu(int id, MenuItem item)
    {
        var menu = await _db.Menus.FirstOrDefaultAsync(m => m.Id == id);

        menu.Items.Add(item);
        await _db.SaveChangesAsync();
    }
}
