using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.Web.Models;

public class BasketItem
{
    public string Name { get; set; }
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtSelection { get; set; }
}