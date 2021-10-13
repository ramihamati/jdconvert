using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Serializer
{
    public class JDTokenStringDeserializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            try
            {
                return jToken.Value<string>();
            }
            catch (Exception )
            {
                return null;
            }

        }
    }
}
