

using FoodOnDelivery.Core.Entities;
using Moq;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.Test;

public class OrderServiceTests
{

    [Fact]
    public async void ValidateInput_ShouldReturnTrue_ForValidInput()
    {
        var mockRepo = new Mock<IRepo<Order>>();
        OrderService _orderService = new(mockRepo.Object);
        // Arrange
        int menuItemId = 9;
        string menuItemName = "Guinness";
        decimal itemPrice = 95.0m;
        int quantity = 1;
        int restaurantId = 4;

        // Act
        bool result = await _orderService.ValidateInput(menuItemId, menuItemName, itemPrice, quantity, restaurantId);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(0, "Guinness", 95.0, 1, 4)]
    [InlineData(9, "", 95.0, 1, 4)]
    [InlineData(9, "Guinness", 0, 1, 4)]
    [InlineData(9, "Guinness", 95.0, 0, 4)]
    [InlineData(9, "Guinness", 95.0, 1, 0)]
    public async void ValidateInput_ShouldReturnFalse_ForInvalidInput(int menuItemId, string menuItemName, decimal itemPrice, int quantity, int restaurantId)
    {
        var mockRepo = new Mock<IRepo<Order>>();
        OrderService _orderService = new(mockRepo.Object);
        // Act
        bool result = await _orderService.ValidateInput(menuItemId, menuItemName, itemPrice, quantity, restaurantId);

        // Assert
        Assert.False(result);
    }
}
