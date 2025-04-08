using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtOrderTime { get; set; } 
    
}