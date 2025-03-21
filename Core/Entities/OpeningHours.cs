namespace FoodOnDelivery.Core.Entities;

public class OpeningHours
{
    public int Id { get; set; }

    // Koppling till restaurangen
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }

    // Vilken veckodag som avses
    public DayOfWeek DayOfWeek { get; set; }

    // Öppnings- och stängningstid för just den dagen
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }
}
