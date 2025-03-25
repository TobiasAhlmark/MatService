using FoodOnDelivery.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class MenuController : Controller
{
    private readonly HttpClient _httpClient;

    public MenuController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IActionResult> Index()
    {
        List<Menu> menus;
        try
        {
            menus = await _httpClient.GetFromJsonAsync<List<Menu>>("http://localhost:5114/api/admin/menu");
        }
        catch
        {
            menus = new List<Menu>();
        }
        return View(menus);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Menu menu)
    {
        var response = await _httpClient.PostAsJsonAsync<Menu>("http://localhost:5114/api/admin/menu", menu);
        if (response.IsSuccessStatusCode)
        {
            await response.Content.ReadFromJsonAsync<Menu>();
            return RedirectToAction("Create");
        }
        return View(menu);
    }




}