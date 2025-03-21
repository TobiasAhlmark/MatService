

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient("MyApi", client =>
       {
           client.BaseAddress = new Uri("https://localhost:7140");
       });
        // builder.Services.AddScoped<IRepo<MenuItem>, ItemRepository>();
        // builder.Services.AddScoped<IRepo<Order>, OrderRepository>();
        // builder.Services.AddScoped<IRepo<Restaurant>, RestaurantRepository>();
        // builder.Services.AddDbContext<AppDbContext>(options =>
        // options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}