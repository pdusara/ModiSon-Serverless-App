public class OrderViewModel
{
    public Order Order { get; set; } = new Order(); // Initialize a blank order
    public List<Product> Products { get; set; } = new List<Product>(); // List of snack names

    // Store JSON string for multiple items submission
    public string ItemsJson { get; set; } = string.Empty;

}