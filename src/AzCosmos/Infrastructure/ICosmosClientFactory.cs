using Microsoft.Azure.Cosmos;

namespace AzCosmos;

public interface ICosmosClientFactory
{
    Func<IServiceProvider, CosmosClient> Create(CosmosConfig config);
}