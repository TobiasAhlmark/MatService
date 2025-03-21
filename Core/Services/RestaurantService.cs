using System.Data.Common;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Core.OperationResult;

namespace FoodOnDelivery.Core.Services;

public class RestaurantService
{
    private readonly IRepo<Restaurant> _repo;

    public RestaurantService(IRepo<Restaurant> repo)
    {
        _repo = repo;
    }

    public async Task<OperationResult<Restaurant>> ValidateNewRestaurant(Restaurant restaurant)
    {
        if (string.IsNullOrWhiteSpace(restaurant.Name))
            return OperationResult<Restaurant>.CreateFailure("Du måste ange ett namn!");

        bool addressExists = await _repo.AnyAsync(r => r.Address == restaurant.Address);
        if (addressExists)
            return OperationResult<Restaurant>.CreateFailure("Adressen finns redan i restauranglistan!");

        await _repo.AddAsync(restaurant);
        
        return OperationResult<Restaurant>.CreateSuccess(restaurant);
    }

    public async Task<OperationResult<Restaurant>> ValidateDelete(int id)
    {
        var restaurant = await _repo.GetByIdAsync(id);

        if (restaurant == null)
        {
            return OperationResult<Restaurant>.CreateFailure($"Ingen restaurang med angivet id{id} hittades!");
        }

        await _repo.DeleteAsync(restaurant);
        return OperationResult<Restaurant>.CreateSuccess(restaurant);
    }
}
