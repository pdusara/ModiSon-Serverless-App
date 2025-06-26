using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : Controller
{
    private readonly DynamoDbService _dynamoDbService;

    public OrdersController(DynamoDbService dynamoDbService)
    {
        _dynamoDbService = dynamoDbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            var orders = await _dynamoDbService.GetOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error seeding products: {ex.Message}");
        }

    }

    // [HttpGet]
    // public async Task<IActionResult> Create()
    // {
    //     var orderViewModel = new OrderViewModel
    //     {
    //         Products = await _dynamoDbService.GetProductsAsync() // List of snack names
    //     };

    //     return View(orderViewModel);
    // }


    [HttpPost]
    public async Task<IActionResult> Create(OrderViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid order data.");
        }

        var order = new Order
        {
            CustomerName = viewModel.Order.CustomerName,
            Username = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "username")?.Value ?? "Anonymous",
            Items = JsonSerializer.Deserialize<List<OrderItem>>(viewModel.ItemsJson)
                    ?? new List<OrderItem>(), // Convert JSON string into list
            Status = "Pending",
            OrderDate = DateTime.UtcNow
        };

        await _dynamoDbService.SaveOrderAsync(order);

        return Ok("Order created successfully.");
    }


    [HttpGet("Get/{id}")]
    public async Task<IActionResult> GetOrder(string id)
    {
        var order = await _dynamoDbService.GetOrderAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteOrder(string id)
    {
        var order = await _dynamoDbService.GetOrderAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        await _dynamoDbService.DeleteOrderAsync(id);
        return Ok($"Order {id} deleted successfully.");
    }

    // public async Task<IActionResult> CustomerOrderList()
    // {
    //     var orders = await _dynamoDbService.GetOrdersAsync();
    //     var customerName = User.Identity?.Name;
    //     if (string.IsNullOrEmpty(customerName))
    //     {
    //         return Unauthorized("User is not authenticated.");
    //     }

    //     var customerOrders = orders
    //         .Where(o => o.Username.Equals(customerName, StringComparison.OrdinalIgnoreCase))
    //         .OrderByDescending(o => o.OrderDate)
    //         .ToList();

    //     if (customerOrders.Count == 0)
    //     {
    //         return NotFound($"No orders found for customer: {customerName}");
    //     }
    //     return View(customerOrders);
    // }
}