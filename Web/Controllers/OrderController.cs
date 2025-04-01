using FoodOnDelivery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Infrastructure.Repositories;

namespace Web.Controllers;

public class OrderController : Controller
{
    private readonly Basket _basket;
    private readonly OrderRepository _orderRepo;
    private readonly CustomerRepository _costumerRepo;
    private readonly OrderItemRepository _orderItemRepo;

    public OrderController
    (
        Basket basket, 
        OrderRepository orderRepository, 
        CustomerRepository customerRepository,
        OrderItemRepository orderItemRepository
    )

    {
        _basket = basket;
        _orderRepo = orderRepository;
        _costumerRepo = customerRepository;
        _orderItemRepo = orderItemRepository;
    }

    public IActionResult Index()
    {
        var basketJson = HttpContext.Session.GetString("Basket");
        Basket basket;
        if (string.IsNullOrEmpty(basketJson))
        {
            basket = new Basket();
        }
        else
        {
            basket = JsonConvert.DeserializeObject<Basket>(basketJson);
        }

        var viewmodel = new BasketViewModel
        {
            Items = basket.Items
        };
        
        return View(viewmodel);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(int menuItemId, string menuItemName, decimal itemPrice, int quantity, int RestaurantId)
    {
        var basketJson = HttpContext.Session.GetString("Basket");
        Basket basket;
        if (string.IsNullOrEmpty(basketJson))
        {
            basket = new Basket();
        }
        else
        {
            basket = JsonConvert.DeserializeObject<Basket>(basketJson);
        }

        var basketItem = new BasketItem
        {
            MenuItemId = menuItemId,
            Name = menuItemName,
            Quantity = quantity,
            PriceAtSelection = itemPrice,
            RestaurantId = RestaurantId
        };

        basket.AddItem(basketItem);

        HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket));

        return RedirectToAction("Index", "Order");
    }

[HttpPost]
public async Task<IActionResult> CreateOrder(string customerName, int customerPhone, string deliveryAddress)
{
    var basketJson = HttpContext.Session.GetString("Basket");
    if (string.IsNullOrEmpty(basketJson))
    {
        return RedirectToAction("Index", "Order");
    }

    var basket = JsonConvert.DeserializeObject<Basket>(basketJson);

    foreach (var item in basket.Items)
    {
        Console.WriteLine($"Id = {item.MenuItemId} - Quantity = {item.Quantity}");
    }
    var customer = new Customer
    {
        Name = customerName,
        PhoneNumber = customerPhone,
        Address = deliveryAddress
    };
    var orderCustomer = await _costumerRepo.AddAsyncWithResponse(customer);
    
    var orderItems = basket.Items.Select(item => new OrderItem
    {
        MenuItemId = item.MenuItemId,
        Quantity = item.Quantity,
        PriceAtOrderTime = item.PriceAtSelection
    }).ToList();

    var order = new Order
    {
        CustomerId = orderCustomer.Id,
        OrderItems = orderItems,
        CourierId = 1,
        RestaurantId = basket.RestaurantId.Value
    };
    Console.WriteLine(order.RestaurantId);
    Console.WriteLine(order.CustomerId);

    Console.WriteLine($"courier = {order.CourierId} - customerId = {order.CustomerId} - restaurangId = {order.RestaurantId}");
    await _orderRepo.AddAsync(order);

    HttpContext.Session.Remove("Basket");

    return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
}

    [HttpGet]
    public async Task<IActionResult> OrderConfirmation(int orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);

        if (order == null)
        {
            return View(order);
        }

        return View(order);
    }
}

