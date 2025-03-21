using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.WebApiController.Costumer;

[ApiController]
[Route("/api/costumer")]
public class CustomerController : ControllerBase
{
    private readonly RestaurantRepository _repoRestaurant;
    private readonly OrderRepository _repoOrder;
    private readonly CustomerRepository _repoCustomer;
    private readonly ItemRepository _repoItem;

    public CustomerController
    (
        RestaurantRepository restaurantRepository,
        OrderRepository orderRepository,
        CustomerRepository customerRepository,
        ItemRepository itemRepository
    )
    {
        _repoRestaurant = restaurantRepository;
        _repoOrder = orderRepository;
        _repoCustomer = customerRepository;
        _repoItem = itemRepository;
    }

    [HttpGet("restaurants")]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _repoRestaurant.GetAllAsync();

        var response = restaurants.Select(restaurant => new RestaurantResponse(
            restaurant.Name,
            restaurant.Address,
            restaurant.Description,
            new MenuResponse(
                restaurant.Menu.Title,
                restaurant.Menu.Items.Select(item => new MenuItemResponse(
                    item.Name,
                    item.Price,
                    item.Description,
                    item.IsVegetarian
                )).ToList()
            )
        ));

        return Ok(response);
    }

    [HttpGet("restaurants/{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurants = await _repoRestaurant.GetByIdAsync(id);
        var restaurant = restaurants;
        var response = new RestaurantResponse(
            restaurant.Name,
            restaurant.Address,
            restaurant.Description,
            new MenuResponse(
                restaurant.Menu.Title,
                restaurant.Menu.Items.Select(item => new MenuItemResponse(
                    item.Name,
                    item.Price,
                    item.Description,
                    item.IsVegetarian
                )).ToList()
            )
        );

        return Ok(response);
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

    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder(OrderRequest request)
    {
        var customer = await _repoCustomer.GetCustomerByPhoneNumber(request.CostumerPhone);

        if (customer == null)
        {
            customer = new Customer
            {
                Name = request.CostumerName,
                PhoneNumber = request.CostumerPhone,
                Address = request.CostumerAddress
            };
            await _repoCustomer.AddAsync(customer);
        }

        var orderItems = new List<OrderItem>();

        foreach (var itemDto in request.OrderItems)
        {
            var menuItem = await _repoItem.GetByIdAsync(itemDto.MenuItemId);
            if (menuItem == null)
            {
                return NotFound($"Item med id {itemDto.MenuItemId} hittades inte!");
            }
            var orderItem = new OrderItem
            {
                MenuItemId = menuItem.Id,
                Quantity = itemDto.Quantity,
                PriceAtOrderTime = menuItem.Price
            };
            orderItems.Add(orderItem);
        }

        var order = new Order
        {
            RestaurantId = request.restaurantId,
            CustomerId = customer.Id,
            OrderItems = orderItems,
            CreatedAt = DateTime.UtcNow
        };
        await _repoOrder.AddAsync(order);

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

public record RestaurantResponse
(
    string Name,
    string Address,
    string Description,
    MenuResponse Menu
);

public record MenuItemResponse(
    string Name,
    decimal Price,
    string Description,
    bool IsVegetarian
);

public record MenuResponse(
    string Title,
    List<MenuItemResponse> Items
);

public record OrderRequest
(
    int restaurantId,
    List<OrderItemDto> OrderItems,
    string CostumerName,
    int CostumerPhone,
    string CostumerAddress
);

public record OrderResponse
(
    Customer Customer,
    List<OrderItem> OrderItems,
    DateTime CreatedAt,
    Courier Courier,
    Restaurant Restaurant
);

public record OrderItemDto(int MenuItemId, int Quantity);

