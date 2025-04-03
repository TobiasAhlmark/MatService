using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalPrice { get; set; }


    public OrderStatus Status { get; set; } = OrderStatus.Received;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? CourierId { get; set; }
    public Courier Courier { get; set; }

    public int RestaurantId { get; set; }
    [JsonIgnore]       // FK
    public Restaurant Restaurant { get; set; } // Navigeringsproperty
    public string RestaurantName => Restaurant.Name;

    public string StatusMessage =>
    Status switch
    {
        OrderStatus.Received => "Order mottagen",
        OrderStatus.Confirmed => "Accepterad, leverans inom 45 min",
        OrderStatus.CourierAccepted => "Budet har accepterat ordern",
        OrderStatus.Preparing => "Ordern förbereds",
        OrderStatus.ReadyForPickup => "Redo för upphämtning",
        OrderStatus.InTransit => "Ordern är på väg",
        OrderStatus.Delivered => "Ordern är levererad",
        _ => "Okänd status"
    };

    public enum OrderStatus
    {
        Received,
        Confirmed,
        CourierAccepted,
        Preparing,
        ReadyForPickup,
        InTransit,
        Delivered
    }
}
