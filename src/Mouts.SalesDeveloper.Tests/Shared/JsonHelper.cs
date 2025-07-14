using System.Text.Json;

namespace Mouts.SalesDeveloper.Tests.Shared
{
    public static class JsonHelper
    {
        public static string Serialize(object obj)
            => JsonSerializer.Serialize(obj);

        public static T Deserialize<T>(string json)
            => JsonSerializer.Deserialize<T>(json)!;
    }
}
