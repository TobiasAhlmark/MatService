namespace FoodOnDelivery.Web.Models
{
    public class BasketViewModel
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        public decimal SubTotal => Items.Sum(item => item.Quantity * item.PriceAtSelection);

        public decimal ServiceFee => SubTotal * 0.05m;

        public decimal DeliveryFee => 50m;

        public decimal TotalCost => SubTotal + ServiceFee + DeliveryFee;
    }
}
