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

    public int RestaurantId { get; set; }       // FK
    public Restaurant Restaurant { get; set; } // Navigeringsproperty

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
