using FoodOnDelivery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Services;

namespace Web.Controllers;

public class OrderController : Controller
{
    private readonly Basket _basket;
    private readonly OrderRepository _orderRepo;
    private readonly CustomerRepository _costumerRepo;
    private readonly OrderItemRepository _orderItemRepo;
    private readonly OrderService _orderService;

    public OrderController
    (
        Basket basket,
        OrderRepository orderRepository,
        CustomerRepository customerRepository,
        OrderItemRepository orderItemRepository,
        OrderService orderService
    )

    {
        _basket = basket;
        _orderRepo = orderRepository;
        _costumerRepo = customerRepository;
        _orderItemRepo = orderItemRepository;
        _orderService = orderService;
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
    public async Task<IActionResult> AddItem(int menuItemId, string menuItemName, decimal itemPrice, int quantity, int restaurantId)
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

        bool validInput = await _orderService.ValidateInput(menuItemId, menuItemName, itemPrice, quantity, restaurantId);

        if(!validInput)
        {
            throw new Exception("Fel värden angavs!");
        }
        else
        {
            var basketItem = new BasketItem
            {
                MenuItemId = menuItemId,
                Name = menuItemName,
                Quantity = quantity,
                PriceAtSelection = itemPrice,
                RestaurantId = restaurantId
            };

            basket.AddItem(basketItem);

            HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket));

            return RedirectToAction("Index", "Order");
        }
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
            TotalPrice = basket.TotalCost,
            RestaurantId = basket.RestaurantId.Value
        };

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
            return NotFound();
        }

        return View(order);
    }

    [HttpGet]
    public async Task<IActionResult> OrderHistory(int phoneNumber)
    {

        var customer = await _costumerRepo.GetCustomerByPhoneNumberasync(phoneNumber);
        if (customer == null)
        {
            ViewBag.Message = "Inga ordrar hittades för det angivna telefonnumret.";
            return View(new List<Order>());
        }

        return View(customer.Orders);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateOrderStatus(int orderId)
    {
        var order = await _orderRepo.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        await _orderRepo.UpdateOrderStatus(order);

        return RedirectToAction("OrderHistory", new { phoneNumber = order.Customer.PhoneNumber });
    }
}

