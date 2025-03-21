namespace FoodOnDelivery.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }

    // Foreign Key till själva Ordern
    public int OrderId { get; set; }
    public Order Order { get; set; }

    // Pekar på vilken MenuItem som beställts
    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }

    public int Quantity { get; set; }
    public decimal PriceAtOrderTime { get; set; } 
    // (valfritt) om du vill spara exakta priset vid ordern
}