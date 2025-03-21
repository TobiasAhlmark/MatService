using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Interfaces;

namespace FoodOnDelivery.Core.Services;

public class CourierService
{
    public void GetOrderToCourier(Order order, int courierId)
    {
        if (order.Status != Order.OrderStatus.Confirmed)
        {
            throw new InvalidOperationException(
                "Kan inte tilldela bud eftersom ordern inte är bekräftad (Status = Confirmed).");
        }

        if (order.CourierId.HasValue)
        {
            throw new InvalidOperationException(
                "Kan inte tilldela bud eftersom ordern redan är tagen av ett annat bud.");
        }

        order.CourierId = courierId;
    }
}