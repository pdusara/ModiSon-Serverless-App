using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Serialization.Json; // For Newtonsoft.Json

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

public class Function
{
    public Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        return Task.FromResult(new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = "Hello from Lambda!",
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        });
    }
}