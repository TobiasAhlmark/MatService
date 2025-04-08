using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }  = "available";
}