using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.WebApiController.Admin;

[ApiController]
[Route("/api/admin/restaurant")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantRepository _repo;
    private readonly RestaurantService _service;

    public RestaurantController(RestaurantRepository repo, RestaurantService restaurantService)
    {
        _repo = repo;
        _service = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurants()
    {
        var restaurants = await _repo.GetAllAsync();
        return Ok(restaurants.Select(restaurants => new GetRestaurantResponse(restaurants.Id, restaurants.Name, restaurants.Address, restaurants.Description)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _repo.GetByIdAsync(id);

        if (restaurant == null)
            return NotFound();

        var response = new RestaurantDetailResponse(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Menu.Items
            .Select(r => new MenuItemDto(r.Id, r.Name, r.Description, r.Price))?
            .ToList() ?? new List<MenuItemDto>(),
            restaurant.Orders?.Select(o => o.Id).ToList() ?? new List<int>()
        );

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantRequest request)
    {
        var restaurant = new Restaurant
        {
            Name = request.Name,
            Address = request.Address,
            Description = request.Description
        };

        var response = await _service.ValidateNewRestaurant(restaurant);

        if (!response.Success)
            return NotFound(response.ErrorMessage);

        return Created(
        $"/api/restaurant/{restaurant.Id}",
        new GetRestaurantResponse(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Description
        )
    );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        var delete = await _service.ValidateDelete(id);

        if (!delete.Success)
            return NotFound(delete.ErrorMessage);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] RestaurantRequest request)
    {

        var restaurant = await _repo.GetByIdAsync(id);
        if (restaurant == null)
            return NotFound();

        restaurant.Name = request.Name;
        restaurant.Address = request.Address;
        restaurant.Description = request.Description;

        await _repo.UpdateAsync(restaurant);

        return Ok(restaurant);
    }

}

public record RestaurantRequest(string Name, string Address, string Description);
public record GetRestaurantResponse(int Id, string Name, string Address, string Description);
public record MenuItemDto(int Id, string Name, string Description, decimal Price);
public record RestaurantDetailResponse(
    int Id,
    string Name,
    string Address,
    List<MenuItemDto> MenuItems,
    List<int> OrderIds
);

