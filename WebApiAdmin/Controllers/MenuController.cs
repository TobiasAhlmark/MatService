using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;

namespace WebAPIControllers.Controllers;

[ApiController]
[Route("/api/admin/menu")]
public class MenuController : ControllerBase
{
    private readonly MenuRepository _repo;

    public MenuController(MenuRepository menuRepository)
    {
        _repo = menuRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenus()
    {
        var menu = await _repo.GetAllAsync();
        return Ok(menu.Select(menu => new GetMenuResponse(menu.Title, menu.Restaurant, menu.Items)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuById(int id)
    {
        var menu = await _repo.GetByIdAsync(id);
        return menu is not null ? Ok(new GetMenuResponse(menu.Title, menu.Restaurant, menu.Items)) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenu(PostMenuRequest request)
    {
        Menu menu = new Menu
        {
            Title = request.Title,
            RestaurantId = request.RestaurantId
        };

        await _repo.AddAsync(menu);
        return Created($"/api/items/{menu.Id}", new GetMenuResponse(menu.Title, menu.Restaurant, new List<MenuItem>()));
    }
}

public record PostMenuRequest(string Title, int RestaurantId);
public record GetMenuResponse(string Title, Restaurant Restaurant, List<MenuItem> Items);