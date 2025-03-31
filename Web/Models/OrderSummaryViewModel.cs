using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.Web.Models;

internal class OrderSummaryViewModel
{
    public List<BasketItem> Items { get; set; }
    public decimal TotalCost { get; set; }
}