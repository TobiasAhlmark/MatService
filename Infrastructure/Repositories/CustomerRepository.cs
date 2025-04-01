using System.Linq.Expressions;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace FoodOnDelivery.Infrastructure.Repositories;

public class CustomerRepository : IRepo<Customer>
{
    private readonly AppDbContext _db;

    public CustomerRepository(AppDbContext appDb)
    {
        _db = appDb;
    }
    public async Task AddAsync(Customer entity)
    {
        var customer = _db.Costumers.FirstOrDefaultAsync(c => c.PhoneNumber == entity.PhoneNumber);
        if(customer != null)
        {
        await _db.Costumers.AddAsync(entity);
        await _db.SaveChangesAsync();
        }
    }

    public Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _db.Costumers.ToListAsync();
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _db.Costumers.FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task UpdateAsync(Customer entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer> GetCustomerByPhoneNumber(int phoneNumber)
    {
        var customer = await _db.Costumers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        return customer;
    }
}