using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter
{
    /// <summary>
    /// Mathing property with the prooer JObject/Token. Then it uses the value reader to get the json value
    /// </summary>
    public class JDTokenReader
    {
        private readonly JDDeserializerManager deserializerManager;
        private readonly JDTokenLookup tokenLookup;

        public JDTokenReader()
        {
            deserializerManager = new JDDeserializerManager();
            tokenLookup = new JDTokenLookup();
        }

        public object ReadValueFromJValue(Type propertyType, JValue jObject)
        {
            if (jObject != null)
            {
                return deserializerManager.GetValueOf(propertyType, jObject);
            }
            return null;
        }

        public object GetValueFor(Type propertyType, JsonPropertyPathAttribute propertyPath, JObject jObject)
        {
            var token = tokenLookup.FindJToken(propertyPath, jObject);

            if (token != null)
            {
                return deserializerManager.GetValueOf(propertyType, token);
            }

            return null;
        }
    }
}
