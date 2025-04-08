using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class Menu
{
    public int Id { get; set; }
    public string Title { get; set; }   
    public int RestaurantId { get; set; }

    [JsonIgnore]
    public Restaurant Restaurant { get; set; }

    public List<MenuItem> Items { get; set; } = new();
}
