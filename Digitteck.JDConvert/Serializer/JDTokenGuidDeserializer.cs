using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Serializer
{
    public sealed class JDTokenGuidDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            string guidValue = jToken.Value<string>();

            bool parsed = Guid.TryParse(guidValue, out Guid guid);

            if (parsed)
            {
                return guid;
            }

            return Guid.Empty;
        }
    }
}
