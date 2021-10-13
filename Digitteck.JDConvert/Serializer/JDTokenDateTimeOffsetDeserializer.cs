using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenDateTimeOffsetDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            if (jToken.Type == JTokenType.String)
            {
                string tokenStrValue = jToken.Value<string>();
                bool op = DateTimeOffset.TryParse(tokenStrValue, out DateTimeOffset tokenValue);
                if (op)
                    return tokenValue;
                return default(DateTimeOffset);
            }
            if (jToken.Type == JTokenType.Date)
            {
                try
                {
                    DateTime dateTime = jToken.Value<DateTime>();
                    return new DateTimeOffset(dateTime);
                }
                catch
                {
                }

                try
                {
                    return jToken.Value<DateTimeOffset>();
                }
                catch
                {
                }
            }

            return default(DateTimeOffset);
        }
    }
}
