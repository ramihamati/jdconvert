using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenBasicDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            return ReturnPrimitive(jToken);
        }

        private static object ReturnPrimitive(JToken jValue)
        {
            if (jValue.Type == JTokenType.Null)
                return null;

            if (jValue.Type == JTokenType.Undefined)
                return null;

            if (jValue.Type == JTokenType.String)
                return jValue.Value<string>();

            if (jValue.Type == JTokenType.Boolean)
                return jValue.Value<bool>();

            if (jValue.Type == JTokenType.Integer)
                return jValue.Value<int>();

            if (jValue.Type == JTokenType.Float)
                return jValue.Value<float>();

            if (jValue.Type == JTokenType.Bytes)
                return jValue.Value<byte[]>();

            if (jValue.Type == JTokenType.Uri)
                return jValue.Value<Uri>();

            if (jValue.Type == JTokenType.TimeSpan)
                return jValue.Value<TimeSpan>();

            if (jValue.Type == JTokenType.Date)
                return jValue.Value<DateTime>();

            return null;
        }
    }
}
