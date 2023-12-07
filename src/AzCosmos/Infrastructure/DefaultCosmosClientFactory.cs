using Azure.Identity;
using Microsoft.Azure.Cosmos;

namespace AzCosmos;

public sealed class DefaultCosmosClientFactory : ICosmosClientFactory
{
    public Func<IServiceProvider, CosmosClient> Create(CosmosConfig config)
    {
        return _ => new CosmosClient(config.Endpoint, new DefaultAzureCredential());
    }
}