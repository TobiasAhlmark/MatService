using Microsoft.AspNetCore.Mvc;
using FoodOnDelivery.Infrastructure.Repositories;
using FoodOnDelivery.Core.Entities;
using FoodOnDelivery.Core.Services;

namespace FoodOnDelivery.WebApiController.Admin;

[ApiController]
[Route("/api/admin/order")]
public class OrderController : ControllerBase
{
    private readonly OrderRepository _repo;
    public OrderController(OrderRepository orderRepository)
    {
        _repo = orderRepository;
    
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _repo.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetOrderByStatus(Order.OrderStatus status, int id)
    {
        var orders = await _repo.GetByStatus(status, id);
        return Ok(orders);
    }

    [HttpPost("status")]
    public async Task<IActionResult> ChangeStatus(int id)
    {
        var response = await _repo.UpdateOrderStatusWithApi(id);
        return Ok(response);
    }

    [HttpPost("courier")]
    public async Task<IActionResult> SetCourierToOrder(int orderId, int courierId)
    {
        var order = await _repo.SetOrderToCourier(orderId, courierId);
        return Ok(order);
    }

    [HttpPost("status/set")]
    public async Task<IActionResult> SetOrderStatus(int orderId, Order.OrderStatus status)
    {
        var order = await _repo.SetOrderStatus(orderId, status);
        return Ok(order);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var order = await _repo.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        else
        {
            await _repo.DeleteAsync(order);
            return Ok("Order deleted successfully");
        }
    }
}