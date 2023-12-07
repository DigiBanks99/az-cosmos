using Microsoft.Azure.Cosmos;

namespace AzCosmos;

internal sealed class EmulatorCosmosClientFactory : ICosmosClientFactory
{
    public Func<IServiceProvider, CosmosClient> Create(CosmosConfig config)
    {
        return _ =>
        {
            CosmosClientOptions cosmosClientOptions = config.Endpoint.Contains("localhost")
                ? new CosmosClientOptions
                {
                    HttpClientFactory = () =>
                    {
                        HttpMessageHandler httpMessageHandler = new HttpClientHandler
                        {
                            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
                        };
                        return new HttpClient(httpMessageHandler);
                    },
                    ConnectionMode = ConnectionMode.Gateway
                }
                : new CosmosClientOptions();

            return new CosmosClient(config.Endpoint, config.Key, cosmosClientOptions);
        };
    }
}