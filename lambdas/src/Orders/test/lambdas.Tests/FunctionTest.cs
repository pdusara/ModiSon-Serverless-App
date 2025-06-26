using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace lambdas.Tests;

public class FunctionTest
{
    [Fact]
    public async void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new Function();
        var context = new TestLambdaContext();
        var request = new Amazon.Lambda.APIGatewayEvents.APIGatewayProxyRequest
        {
            Body = "hello world"
        };
        var upperCase = await function.FunctionHandler(request, context);

        Assert.Equal("HELLO WORLD", upperCase.Body);
    }
}
