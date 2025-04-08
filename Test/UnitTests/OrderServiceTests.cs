using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.Test;

public class OrderServiceTests
{
    [Theory]
    [InlineData(0, "Guinness", 95.0, 1, 4)]
    [InlineData(9, "", 95.0, 1, 4)]
    [InlineData(9, "Guinness", 0, 1, 4)]
    [InlineData(9, "Guinness", 95.0, 0, 4)]
    [InlineData(9, "Guinness", 95.0, 1, 0)]
    public async Task ValidateInput_ShouldReturnFalse_ForInvalidInput(int menuItemId, string menuItemName, decimal itemPrice, int quantity, int restaurantId)
    {
        var orderService = new OrderService();

        bool result = await orderService.ValidateInput(menuItemId, menuItemName, itemPrice, quantity, restaurantId);

        Assert.False(result);
    }

}
