using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.SerializerBase
{
    public interface IJDDeserializer
    {
        object Convert(JToken jToken);
    }
}