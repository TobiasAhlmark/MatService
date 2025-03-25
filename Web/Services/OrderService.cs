using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.Web.Services;

public class OrderService : IOrderService
{
    private readonly List<OrderItem> _orderItems = new();
    public void AddItem(OrderItem item)
    {
        _orderItems.Add(item);
    }

    public decimal CalculateTotal()
    {
        return _orderItems.Sum(item => item.PriceAtOrderTime * item.Quantity);
    }

    public List<OrderItem> GetCurrentOrderItems()
    {
        return _orderItems;
    }
}