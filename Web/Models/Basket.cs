using System.Security.Principal;
using FoodOnDelivery.Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FoodOnDelivery.Web.Models;

public class Basket
{
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    public int? RestaurantId { get; set; }
    public decimal SubTotal => Items.Sum(item => item.Quantity * item.PriceAtSelection);
    public decimal ServiceFee => SubTotal * 0.05m;
    public decimal DeliveryFee => 50m;
    public decimal TotalCost => SubTotal + ServiceFee + DeliveryFee;

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
        }
    }

}