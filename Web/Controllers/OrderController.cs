using FoodOnDelivery.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers;

public class OrderController : Controller
{
    private readonly Basket _basket;

    public OrderController(Basket basket)
    {
        _basket = basket;
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
        foreach (var item in viewmodel.Items)
        {
            Console.WriteLine($"{item.Name} - {item.MenuItemId} - {item.PriceAtSelection}");
        }
        return View(viewmodel);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(int menuItemId, string name, decimal itemPrice, int quantity)
    {
        var basketJson = HttpContext.Session.GetString("Basket");
        Basket basket;
        if (string.IsNullOrEmpty(basketJson))
        {
            // Om ingen Basket finns, skapa en ny
            basket = new Basket();
        }
        else
        {
            // Deserialisera Basket från sessionen
            basket = JsonConvert.DeserializeObject<Basket>(basketJson);
        }

        var basketItem = new BasketItem
        {
            MenuItemId = menuItemId,
            Name = name,
            Quantity = quantity,
            PriceAtSelection = itemPrice
        };

        basket.AddItem(basketItem);

        HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket));

        return RedirectToAction("Index", "Order");
    }

    // await _httpClient.PostAsJsonAsync<int>("http://localhost:5250/api/customer/restaurant", restaurant);




}

