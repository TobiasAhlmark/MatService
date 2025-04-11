using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;

namespace FoodOnDelivery.Core.Services;

public class OrderService
{

    public async Task<bool> ValidateInput(int menuItemId, string menuItemName, decimal itemPrice, int quantity, int restaurantId)
    {
        if (menuItemId <= 0)
            return false;

        if (string.IsNullOrWhiteSpace(menuItemName))
            return false;

        if (itemPrice <= 0)
            return false;

        if (quantity <= 0)
            return false;

        if (restaurantId <= 0)
            return false;

        return true;
    }

}