using System;
using System.Reflection;
using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.PropertyBinderBase;
using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.PropertyBinders
{
    public class JDPropertyAttributeBinder : IJDPropertyBinder
    {
        private readonly JDTokenLookup jTokenLookup;

        public bool ValueSolved;

        public JDPropertyAttributeBinder()
        {
            jTokenLookup = new JDTokenLookup();
        }

        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            if (!propertyInfo.CanWrite)
            {
                ValueSolved = false;
                return true;
            }

            if (propertyInfo.GetCustomAttribute<JDDeserializerAttribute>() is JDDeserializerAttribute attr)
            {
                Type serializerType = attr.Serializer;

                IJDDeserializer deserializer = Activator.CreateInstance(serializerType) as IJDDeserializer;

                if (deserializer != null)
                {
                    var tokenForProperty = jTokenLookup.FindJToken(propertyPath, parentJson);

                    if (tokenForProperty != null)
                    {
                        object propertyValue = deserializer.Convert(tokenForProperty);

                        if (propertyValue != null)
                        {
                            propertyInfo.SetValue(parentModel, propertyValue);
                            ValueSolved = true;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
