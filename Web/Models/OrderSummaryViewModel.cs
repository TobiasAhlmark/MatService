using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.Web.Models;

internal class OrderSummaryViewModel
{
    public List<OrderItem> Items { get; set; }
    public decimal TotalCost { get; set; }
}