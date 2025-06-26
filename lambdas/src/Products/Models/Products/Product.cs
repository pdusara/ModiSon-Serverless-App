using Amazon.DynamoDBv2.DataModel;

[DynamoDBTable("Products")]
public class Product
{
    [DynamoDBHashKey] // Partition key
    public string ProductId { get; set; } = Guid.NewGuid().ToString();

    [DynamoDBProperty]
    public string ProductName { get; set; } = string.Empty;

    [DynamoDBProperty]
    public decimal Price { get; set; }

    [DynamoDBProperty]
    public string Category { get; set; } = string.Empty;
}