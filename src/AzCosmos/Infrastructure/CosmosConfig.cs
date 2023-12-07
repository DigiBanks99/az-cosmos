namespace AzCosmos;

public sealed class CosmosConfig
{
    private string? _key;
    public string Endpoint { get; set; } = "https://localhost:8081";

    public string? Key
    {
        get
        {
            if (string.IsNullOrEmpty(_key) || !UseAzure)
            {
                return "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
            }

            return _key;
        }
        set => _key = value;
    }

    public bool UseAzure { get; set; } = true;
}