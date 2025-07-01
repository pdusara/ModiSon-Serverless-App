using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : Controller
{
    private readonly DynamoDbService _dynamoDbService;

    public ProductsController(DynamoDbService dynamoDbService)
    {
        _dynamoDbService = dynamoDbService;
    }


    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            // Extract the claims from the authenticated user
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            Console.WriteLine("Incoming JWT claims:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // Optional: pull a specific claim like the Cognito username
            var username = User.FindFirst("cognito:username")?.Value
                        ?? User.Identity?.Name;

            Console.WriteLine($"Authenticated user: {username}");

            var products = await _dynamoDbService.GetProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error getting products: {ex.Message}");
        }
    }

}