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
            var products = await _dynamoDbService.GetProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error getting products: {ex.Message}");
        }

    }

}