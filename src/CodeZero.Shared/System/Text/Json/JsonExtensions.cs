namespace System.Text.Json;

public static class JsonExtensions
{
    public static T FromJson<T>(
        this string json,
        JsonSerializerOptions jsonOptions = default!)
    {
        var josnDeserialize = JsonSerializer.Deserialize<T>(json, jsonOptions);

        if (josnDeserialize is not null)
            return josnDeserialize;

        return default!;
    }

    public static string ToJson<T>(this T obj, JsonSerializerOptions jsonOptions = default!) => JsonSerializer.Serialize<T>(obj, jsonOptions);
}