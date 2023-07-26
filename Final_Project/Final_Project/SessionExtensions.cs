// CartItem model (adjust as per your actual model)
using System.Text.Json;

public class CartItem
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public int? Quantity { get; set; }
    public double? Price { get; set; }
}

// Session extension methods for serialization
public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
