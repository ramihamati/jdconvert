using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenFloatDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            if (jToken.Type == JTokenType.String)
            {
                string tokenStrValue = jToken.Value<string>();

                bool op = float.TryParse(tokenStrValue, out float tokenIntValue);

                if (op)
                    return tokenIntValue;

                return default(float);
            }

            if (jToken.Type == JTokenType.Float)
            {
                return jToken.Value<float>();
            }

            if (jToken.Type == JTokenType.Integer)
            {
                return jToken.Value<int>();
            }
            try
            {
                return (float)jToken.Value<double>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
