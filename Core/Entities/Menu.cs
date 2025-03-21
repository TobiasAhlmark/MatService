using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class Menu
{
    public int Id { get; set; }
    public string Title { get; set; }         // Exempel: "Lunchmeny", "Frukostmeny" etc.
    public int RestaurantId { get; set; }     // FK till Restaurant

    // Navigeringsproperty tillbaka 
    [JsonIgnore]
    public Restaurant Restaurant { get; set; }

    // Om du även vill ha menyalternativ
    public List<MenuItem> Items { get; set; } = new();
}
