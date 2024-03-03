namespace BDTheque.Tests.Extensions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class StringExtensions
{
    public static Guid GetValue(this string s, string valuePath)
    {
        if (JsonConvert.DeserializeObject<JObject>(s) is not { } jsonObject) return Guid.Empty;
        return jsonObject.SelectToken(valuePath) is { } entityId ? entityId.ToObject<Guid>() : Guid.Empty;
    }
}
