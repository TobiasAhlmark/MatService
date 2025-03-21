namespace FoodOnDelivery.Core.Entities;

public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }  = "available";

    public List<Order> Orders { get; set; } = new();
}