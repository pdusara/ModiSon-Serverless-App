// csharp
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Moq;
using Xunit;

public class DynamoDbServiceTest
{
    [Fact]
    public async Task GetProductsAsync_ReturnsProducts()
    {
        // Arrange
        var mockDynamoDb = new Mock<IAmazonDynamoDB>();
        var mockContext = new Mock<IDynamoDBContext>();
        var expectedProducts = new List<Product>
        {
            new Product { ProductId = "1", ProductName = "Samosa", Price = 2.50m, Category = "Snack" }
        };

        var mockAsyncSearch = new Mock<AsyncSearch<Product>>();
        mockAsyncSearch.Setup(x => x.GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedProducts);

        mockContext.Setup(x => x.ScanAsync<Product>(It.IsAny<List<ScanCondition>>()))
            .Returns(mockAsyncSearch.Object);

        var service = new DynamoDbService(mockDynamoDb.Object, mockContext.Object);

        // Act
        var result = await service.GetProductsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Samosa", result[0].ProductName);
    }

    [Fact]
    public async Task SeedProductsAsync_Seeds_WhenEmpty()
    {
        // Arrange
        var mockDynamoDb = new Mock<IAmazonDynamoDB>();
        var mockContext = new Mock<IDynamoDBContext>();

        var emptyList = new List<Product>();
        var mockAsyncSearch = new Mock<AsyncSearch<Product>>();
        mockAsyncSearch.Setup(x => x.GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyList);

        mockContext.Setup(x => x.ScanAsync<Product>(It.IsAny<List<ScanCondition>>()))
            .Returns(mockAsyncSearch.Object);

        mockContext.Setup(x => x.SaveAsync(It.IsAny<Product>(), default))
            .Returns(Task.CompletedTask);

        var service = new DynamoDbService(mockDynamoDb.Object, mockContext.Object);

        // Act
        await service.SeedProductsAsync();

        // Assert
        mockContext.Verify(x => x.SaveAsync(It.Is<Product>(p => p.ProductId == "1"), default), Times.Once);
        mockContext.Verify(x => x.SaveAsync(It.Is<Product>(p => p.ProductId == "2"), default), Times.Once);
    }

    [Fact]
    public async Task SeedProductsAsync_DoesNotSeed_WhenNotEmpty()
    {
        // Arrange
        var mockDynamoDb = new Mock<IAmazonDynamoDB>();
        var mockContext = new Mock<IDynamoDBContext>();

        var existingList = new List<Product>
        {
            new Product { ProductId = "1", ProductName = "Samosa", Price = 2.50m, Category = "Snack" }
        };
        var mockAsyncSearch = new Mock<AsyncSearch<Product>>();
        mockAsyncSearch.Setup(x => x.GetRemainingAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingList);

        mockContext.Setup(x => x.ScanAsync<Product>(It.IsAny<List<ScanCondition>>()))
            .Returns(mockAsyncSearch.Object);

        var service = new DynamoDbService(mockDynamoDb.Object, mockContext.Object);

        // Act
        await service.SeedProductsAsync();

        // Assert
        mockContext.Verify(x => x.SaveAsync(It.IsAny<Product>(), default), Times.Never);
    }
}
