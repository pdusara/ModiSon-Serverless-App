using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;

namespace Products;

public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
{
    protected override void Init(IWebHostBuilder builder)
    {
        builder.UseStartup<Startup>();
    }

    public override async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandlerAsync(
        APIGatewayHttpApiV2ProxyRequest request,
        ILambdaContext context)
    {
        Console.WriteLine("Incoming event:");
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(request));

        return await base.FunctionHandlerAsync(request, context);
    }
}
