using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        // Add other services here
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IDynamoDBContext>(sp =>
        {
            var client = sp.GetRequiredService<IAmazonDynamoDB>();
            return new DynamoDBContext(client);
        });
        services.AddSingleton<DynamoDbService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            // Ensure the DynamoDB table is created and seeded
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dynamoDbService = scope.ServiceProvider.GetRequiredService<DynamoDbService>();
                // Block on the async call for seeding
                dynamoDbService.SeedProductsAsync().GetAwaiter().GetResult();
            }
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}