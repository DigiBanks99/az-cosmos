using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzCosmos;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTodoCollection(
        this IServiceCollection services,
        IConfiguration config,
        IHostEnvironment env)
    {
        CosmosConfig cosmosConfig = new();
        config.GetSection("Cosmos").Bind(cosmosConfig);

        ICosmosClientFactory cosmosClientFactory = env.IsProduction() || cosmosConfig.UseAzure
            ? new DefaultCosmosClientFactory()
            : new EmulatorCosmosClientFactory();

        return services
            .AddSingleton<CosmosClient>(cosmosClientFactory.Create(cosmosConfig))
            .AddSingleton<TodosCollection>();
    }
}