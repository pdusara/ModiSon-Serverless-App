using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Orders")]
public class Order
{
    [DynamoDBHashKey] // Partition key
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    [DynamoDBProperty]
    public string Username { get; set; } = string.Empty; // Username of the customer
    [DynamoDBProperty]
    public string CustomerName { get; set; } = string.Empty;
    [DynamoDBProperty]
    public string Status { get; set; } = string.Empty; // e.g., "Pending", "Shipped", "Delivered"
    [DynamoDBProperty]
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    [DynamoDBProperty]
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
}