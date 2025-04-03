namespace FoodOnDelivery.Web.Models
{
    public class BasketViewModel
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

        // Delbeloppet för alla varor
        public decimal SubTotal => Items.Sum(item => item.Quantity * item.PriceAtSelection);

        // 5% serviceavgift
        public decimal ServiceFee => SubTotal * 0.05m;

        // Fast leveransavgift på 50 kr
        public decimal DeliveryFee => 50m;

        // Total kostnad med alla avgifter
        public decimal TotalCost => SubTotal + ServiceFee + DeliveryFee;
    }
}
