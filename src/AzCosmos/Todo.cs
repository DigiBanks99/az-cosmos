using Newtonsoft.Json;

namespace AzCosmos;

public class Todo
{
    [JsonProperty("category")] public string Category { get; set; }

    [JsonProperty("id")] public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("description")] public string Description { get; set; }

    [JsonProperty("status")] public Status Status { get; set; }

    public override string ToString()
    {
        return $"{Description}:\t{Status}";
    }
}