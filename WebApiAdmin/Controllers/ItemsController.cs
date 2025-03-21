using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;

namespace FoodOnDelivery.WebAPIControllers.Admin;

[ApiController]
[Route("/api/admin/items")]
public class ItemsController : ControllerBase
{
    private readonly ItemRepository _repo;

    public ItemsController(ItemRepository itemsRepository)
    {
        _repo = itemsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var items = await _repo.GetAllAsync();
        return Ok(items.Select(item => new GetItemResponse(item.Name, item.Price, item.Description, item.IsVegetarian)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is not null ? Ok(new GetItemResponse(item.Name, item.Price, item.Description, item.IsVegetarian)) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(PostItemRequest request)
    {
        var item = new MenuItem(request.Name, request.Price, request.Description, request.IsVegetarian, request.MenuId);
        await _repo.AddAsync(item);
        return Created($"/api/items/{item.Id}", new GetItemResponse(item.Name, item.Price, item.Description, item.IsVegetarian));
    }
}

public record PostItemRequest(string Name, decimal Price, string Description, bool IsVegetarian, int MenuId);
public record GetItemResponse(string Name, decimal Price, string Description, bool IsVegetarian);