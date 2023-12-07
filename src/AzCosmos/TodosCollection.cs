using Microsoft.Azure.Cosmos;
using System.Runtime.CompilerServices;

namespace AzCosmos;

public sealed class TodosCollection
{
    private readonly CosmosClient _cosmosClient;
    private Container? _container;
    private Database? _store;

    public TodosCollection(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient;
    }

    private Container Collection =>
        _container ?? throw new InvalidOperationException("Please initialize the collection");

    private Database Store => _store ?? throw new InvalidOperationException("Please initialize the collection");

    public void Initialize()
    {
        _store = CreateDatabase();
        _container = CreateCollection();
    }

    public Task AddAsync(Todo todo, CancellationToken cancellationToken)
    {
        return Collection.CreateItemAsync(todo, new PartitionKey(todo.Category), cancellationToken: cancellationToken);
    }

    public async IAsyncEnumerable<Todo> GetAllAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        FeedIterator<Todo> iterator = Collection.GetItemQueryIterator<Todo>();

        while (iterator.HasMoreResults && !cancellationToken.IsCancellationRequested)
        {
            FeedResponse<Todo>? batch = await iterator.ReadNextAsync(cancellationToken);
            foreach (Todo todo in batch)
            {
                yield return todo;
            }
        }
    }

    public async IAsyncEnumerable<Todo> GetAllByCategoryAsync(string category,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        QueryDefinition query = new QueryDefinition("select * from todos t where t.category = @cateogory")
            .WithParameter("@category", category);
        FeedIterator<Todo> iterator = Collection.GetItemQueryIterator<Todo>(
            query,
            null,
            new QueryRequestOptions
            {
                PartitionKey = new PartitionKey("/category")
            });

        while (iterator.HasMoreResults && !cancellationToken.IsCancellationRequested)
        {
            FeedResponse<Todo>? batch = await iterator.ReadNextAsync(cancellationToken);
            foreach (Todo todo in batch)
            {
                yield return todo;
            }
        }
    }

    public async Task<Todo?> GetTodoAsync(string? id, CancellationToken cancellationToken)
    {
        ItemResponse<Todo>? response = await Collection.ReadItemAsync<Todo>(id, new PartitionKey("home"), cancellationToken: cancellationToken);

        return response.Resource;
    }

    public Task UpdateTodoAsync(Todo todo, CancellationToken cancellationToken)
    {
        return Collection.UpsertItemAsync(todo, cancellationToken: cancellationToken);
    }

    private Database CreateDatabase()
    {
        return _cosmosClient.GetDatabase(Constants.TodoAppKey);
    }

    private Container CreateCollection()
    {
        return Store.GetContainer("Todos");
    }
}