using Microsoft.EntityFrameworkCore;
using FoodOnDelivery.Core.Entities;
namespace FoodOnDelivery.Infrastructure.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        // Exempel: om du har en Restaurant-klass i Core:
        public DbSet<Restaurant> Restaurants { get; set; } 
        public DbSet<Customer> Costumers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

