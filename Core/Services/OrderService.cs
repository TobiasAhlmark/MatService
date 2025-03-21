using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;

namespace FoodOnDelivery.Core.Services;

public class OrderService
{
      private readonly IRepo<Order> _repo;

    public OrderService(IRepo<Order> repo)
    {
        _repo = repo;
    }

    //TODO Fixa knappar som väljer vilken status i UI
    public async Task GetAllOrdersWithSpecifikStatus (Order.OrderStatus status)
    {
        var allOrders = await _repo.GetAllAsync();
        var OrderWithStatus = allOrders.Where(r => r.Status == status);
    }
}