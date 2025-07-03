using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

public interface IDynamoDbService
{
    Task DeleteOrderAsync(string id);
    Task<Order?> GetOrderAsync(string id);
    Task<List<Order>> GetOrdersAsync(string? username);
    Task SaveOrderAsync(Order order);
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

    public async Task SaveOrderAsync(Order order)
    {
        await _context.SaveAsync(order);
    }

    public async Task<List<Order>> GetOrdersAsync(string? username)
    {
        // If username is provided, filter orders by username
        if (!string.IsNullOrEmpty(username))
        {
            var conditions = new List<ScanCondition>
            {
                new ScanCondition("Username", ScanOperator.Equal, username)
            };
            var orders = await _context.ScanAsync<Order>(conditions).GetRemainingAsync();
            return orders.OrderByDescending(o => o.OrderDate).ToList();
        }

        return new List<Order>(); // Return an empty list if no username is provided
    }

    public async Task<Order?> GetOrderAsync(string id)
    {
        return await _context.LoadAsync<Order>(id);
    }

    public async Task DeleteOrderAsync(string id)
    {
        await _context.DeleteAsync<Order>(id);
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