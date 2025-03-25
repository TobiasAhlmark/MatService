using FoodOnDelivery.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers;

public class RestaurantController : Controller
{
    private readonly ILogger<RestaurantController> _logger;
    private readonly HttpClient _httpClient;

    public RestaurantController(ILogger<RestaurantController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        List<Restaurant> restaurants;
        try
        {
            restaurants = await _httpClient.GetFromJsonAsync<List<Restaurant>>("http://localhost:5250/api/customer/restaurants");
        }
        catch (Exception ex)
        {
            restaurants = new List<Restaurant>();
        }

        // Om listan är tom skickar vi den vidare till vyn
        return View(restaurants);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Menu(int id)
    {
        try
        {
            var restaurant = await _httpClient.GetFromJsonAsync<Restaurant>($"http://localhost:5250/api/customer/restaurants/{id}");

            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "API-tjänsten är inte tillgänglig just nu.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ett oväntat fel inträffade i RestaurantById");
            return StatusCode(StatusCodes.Status500InternalServerError, "Ett oväntat fel inträffade.");
        }
    }

    // POST: Restaurant/Create
    // Tar emot data från formuläret och skickar vidare till det externa API:t
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Restaurant restaurant)
    {
        var response = await _httpClient.PostAsJsonAsync<Restaurant>("http://localhost:5114/api/admin/restaurant", restaurant);
        if (response.IsSuccessStatusCode)
        {
            var createdRestaurant = await response.Content.ReadFromJsonAsync<Restaurant>();
            return RedirectToAction("Create");
        }
        return View(restaurant);
    }

    // GET: Restaurant/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete()
    {
        List<Restaurant> restaurants = null;
        try
        {
            restaurants = await _httpClient.GetFromJsonAsync<List<Restaurant>>("http://localhost:5114/api/admin/restaurant");
        }
        catch (Exception ex)
        {
            restaurants = new List<Restaurant>();
        }

        // Om listan är tom skickar vi den vidare till vyn
        return View(restaurants);
    }

    // POST: Restaurant/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {

        var response = await _httpClient.DeleteAsync($"http://localhost:5114/api/admin/restaurant/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Kunde inte radera restaurangen.");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {

        List<Restaurant> restaurants = null;
        try
        {
            restaurants = await _httpClient.GetFromJsonAsync<List<Restaurant>>("http://localhost:5114/api/admin/restaurant");
        }
        catch (Exception ex)
        {
            restaurants = new List<Restaurant>();
        }

        // Om listan är tom skickar vi den vidare till vyn
        return View(restaurants);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(int id, Restaurant restaurant)
    {
        if (!ModelState.IsValid)
        {
            return View(restaurant);
        }

        // Skicka PUT-anrop till API:t med uppdaterade data
        var response = await _httpClient.PutAsJsonAsync($"http://localhost:5114/api/admin/restaurant/{id}", restaurant);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError("", "Kunde inte uppdatera restaurangen.");
        return View(restaurant);
    }
}

