using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenIntDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            if (jToken.Type == JTokenType.String)
            {
                string tokenStrValue = jToken.Value<string>();

                bool op = int.TryParse(tokenStrValue, out int tokenIntValue);

                if (op)
                    return tokenIntValue;

                return default(int);
            }

            try
            {
                return jToken.Value<int>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
