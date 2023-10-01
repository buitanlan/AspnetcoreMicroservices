using System.Text.Json;
using Contracts.Common.Interfaces;

namespace Infrastructure.Common;

public class SerializeService: ISerializeService
{
    private static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web)
    {
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    public string Serialize<T>(T obj) => JsonSerializer.Serialize(obj,Options);

    public string Serialize<T>(T obj, Type type) => JsonSerializer.Serialize(obj, type, Options);

    public T Deserialize<T>(string json) => JsonSerializer.Deserialize<T>(json, Options);
}
