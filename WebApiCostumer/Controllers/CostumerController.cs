using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.WebApiController.Costumer;

[ApiController]
[Route("/api/costumer")]
public class CostumerController : ControllerBase
{
    private readonly RestaurantRepository _repoRestaurant;
    private readonly OrderRepository _repoOrder;

    public CostumerController
    (
        RestaurantRepository restaurantRepository,
        OrderRepository orderRepository
    )
    {
        _repoRestaurant = restaurantRepository;
        _repoOrder = orderRepository;
    }

    [HttpGet("restaurants")]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _repoRestaurant.GetAllAsync();
        return Ok(restaurants.Select(restaurant => new RestaurantResponse
        (
            restaurant.Name, 
            restaurant.Address, 
            restaurant.Description, 
            restaurant.Menu
        )));
    }

    [HttpGet("restaurants/{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _repoRestaurant.GetByIdAsync(id);
        return Ok(new RestaurantResponse
        (
            restaurant.Name,
            restaurant.Address,
            restaurant.Description,
            restaurant.Menu
        ));
    }

    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _repoOrder.GetAllAsync();
        return Ok(orders.Select(order =>
        new OrderResponse
        (
            order.Customer,
            order.OrderItems,
            order.CreatedAt,
            order.Courier,
            order.Restaurant
        )));
    }

    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _repoOrder.GetByIdAsync(id);
        return Ok(new OrderResponse
        (
            order.Customer,
            order.OrderItems,
            order.CreatedAt,
            order.Courier,
            order.Restaurant
        ));
    }
}

public record RestaurantResponse(string Name, string Address, string Description, Menu Menu);
public record OrderResponse
(
    Customer Customer,
    List<OrderItem> OrderItems,
    DateTime CreatedAt,
    Courier Courier,
    Restaurant Restaurant
);