namespace FoodOnDelivery.Core.Entities;

public class Payment
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public string? TransactionId { get; set; }
}

public enum PaymentMethod
{
    CreditCard,
    Swish,
    PayPal,
    Other
}

public enum PaymentStatus
{
    Pending,
    Completed,
    Failed
}
