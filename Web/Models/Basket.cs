using System.Security.Principal;
using FoodOnDelivery.Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FoodOnDelivery.Web.Models;

public class Basket
{
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public int? RestaurantId { get; set; }

    public void AddItem(BasketItem newItem)
    {
        if (Items.Count == 0)
        {
            RestaurantId = newItem.RestaurantId;
        }
        else if (RestaurantId != newItem.RestaurantId)
        {
            throw new Exception("Du kan endast beställa från en restaurang per order.");
        }

        var existingItem = Items.FirstOrDefault(i => i.MenuItemId == newItem.MenuItemId);
        if (existingItem != null)
        {
            existingItem.Quantity += newItem.Quantity;
        }
        else
        {
            Items.Add(newItem);
            foreach (var item in Items)
            {
                Console.WriteLine(item.Quantity);
            }
        }
    }

    public decimal GetTotalCost()
    {
        return Items.Sum(i => i.Quantity * i.PriceAtSelection);
    }

    public List<BasketItem> GetCurrentBasketItems()
    {
        return Items;
    }
}