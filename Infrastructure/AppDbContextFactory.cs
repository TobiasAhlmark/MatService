using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FoodOnDelivery.Infrastructure.DB;

namespace FoodOnDelivery.Infrastructure.DB
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlite("Data Source=FoodOnDelivery.db");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
