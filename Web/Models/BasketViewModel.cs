namespace FoodOnDelivery.Web.Models
{
    public class BasketViewModel
    {
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public decimal TotalCost 
        { 
            get { return Items.Sum(item => item.Quantity * item.PriceAtSelection); } 
        }
    }
}
