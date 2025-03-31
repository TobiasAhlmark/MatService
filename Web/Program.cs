using FoodOnDelivery.Web.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpClient("MyApi", client =>
       {
           client.BaseAddress = new Uri("https://localhost:7140");
       });

       builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
       {
           options.IdleTimeout = TimeSpan.FromMinutes(30);
           options.Cookie.HttpOnly = true;
           options.Cookie.IsEssential = true;
       });
        builder.Services.AddScoped<Basket>();
        builder.Services.AddScoped<BasketItem>();


        var app = builder.Build();

        app.UseSession();
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