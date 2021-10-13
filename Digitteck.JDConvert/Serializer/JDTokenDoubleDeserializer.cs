using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenDoubleDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            if (jToken.Type == JTokenType.String)
            {
                string tokenStrValue = jToken.Value<string>();

                bool op = double.TryParse(tokenStrValue, out double tokenIntValue);

                if (op)
                    return tokenIntValue;

                return default(double);
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
                return jToken.Value<double>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
