using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FoodOnDelivery.Infrastructure.DB;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Interfaces;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Services;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodOnDelivery.Infrastructure.ServiceCollection;

public static class InfrastructureServiceCollectionExtensions 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=C:/Users/hagas/OneDrive/Dokument/Suvnet-24/OOP2/MatService-TobiasAhlmark/Infrastructure/FoodOnDelivery.db"));

        services.AddScoped<IRepo<Restaurant>, RestaurantRepository>();
        services.AddScoped<RestaurantRepository>();
        services.AddScoped<RestaurantService>();

        services.AddScoped<IRepo<Menu>, MenuRepository>();
        services.AddScoped<MenuRepository>();

        services.AddScoped<IRepo<MenuItem>, ItemRepository>();
        services.AddScoped<ItemRepository>();

        services.AddScoped<IRepo<Customer>, CustomerRepository>();
        services.AddScoped<CustomerRepository>();

        services.AddScoped<IRepo<Order>, OrderRepository>();
        services.AddScoped<OrderRepository>();


        // Registrera andra infrastruktur-relaterade tjänster här om du vill
        return services;
    }
}