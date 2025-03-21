using System.Text.Json.Serialization;

namespace FoodOnDelivery.Core.Entities;

public class MenuItem
{
    public MenuItem(string name, decimal price, string description, bool isVegetarian, int menuId)
    {
        Name = name;
        Price = price;
        Description = description;
        IsVegetarian = isVegetarian;
        MenuId = menuId;
    }

    public int Id { get; set; }         // Primärnyckel

    public string Name { get; set; }    // Namn på rätten, t.ex. "Pizza Margherita"
    public decimal Price { get; set; }  // Pris

  
    public string Description { get; set; }
    public bool IsVegetarian { get; set; }
   
    public int MenuId { get; set; }
    
    [JsonIgnore]
    public Menu Menu { get; set; }
}
