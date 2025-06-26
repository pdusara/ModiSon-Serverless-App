using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

public interface IDynamoDbService
{
    Task<List<Product>> GetProductsAsync();
    Task SeedProductsAsync();
}

public class DynamoDbService : IDynamoDbService
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    private readonly IDynamoDBContext _context;

    public DynamoDbService(IAmazonDynamoDB dynamoDbClient, IDynamoDBContext context)
    {
        _dynamoDbClient = dynamoDbClient;
        _context = context;
    }


    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
    }

    public async Task SeedProductsAsync()
    {
        var existingProducts = await _context.ScanAsync<Product>(new List<ScanCondition>()).GetRemainingAsync();
        if (existingProducts.Count != 0) return; // Prevent duplicate inserts

        // Seed initial products if the table is empty
        var products = new List<Product>
        {
            new Product { ProductId = "1", ProductName = "Samosa", Price = 2.50m, Category = "Snack" },
            new Product { ProductId = "2", ProductName = "Jalebi", Price = 3.00m, Category = "Sweet" }
        };

        foreach (var product in products)
        {
            await _context.SaveAsync(product);
        }
    }
}