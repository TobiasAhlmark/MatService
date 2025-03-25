using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Web.Models;
using FoodOnDelivery.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class OrderController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IOrderService _orderService;

    public OrderController(HttpClient httpClient, IOrderService service)
    {
        _httpClient = httpClient;
        _orderService = service;
    }

    public IActionResult OrderSummary()
    {
        var items = _orderService.GetCurrentOrderItems();
        var total = _orderService.CalculateTotal();
        // Skicka med informationen till vyn
        var viewModel = new OrderSummaryViewModel
        {
            Items = items,
            TotalCost = total
        };
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult AddItems(OrderItem orderItem)
    {
        try
        {
            _orderService.AddItem(orderItem);

            return RedirectToAction("OrderSummary");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Något gick fel när ordern lades till.");
        }
    }

    public IActionResult OrderSidebar()
    {
        var items = _orderService.GetCurrentOrderItems();
        var total = _orderService.CalculateTotal();
        var viewModel = new OrderSummaryViewModel
        {
            Items = items,
            TotalCost = total
        };
        return PartialView("_OrderSidebar", viewModel);
    }


}

