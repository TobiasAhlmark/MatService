using Microsoft.AspNetCore.Mvc.Testing;
using FoodOnDelivery.Core.Entities;
using System.Net.Http.Json;

namespace FoodOnDelivery.Test;

public class IntegrationTestAdminApi : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public IntegrationTestAdminApi(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllOrders_ReturnsOrderListAndOkStatus()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/admin/order");
        response.EnsureSuccessStatusCode();

        var orders = await response.Content.ReadFromJsonAsync<List<Order>>();
        Assert.NotNull(orders);
    }
}