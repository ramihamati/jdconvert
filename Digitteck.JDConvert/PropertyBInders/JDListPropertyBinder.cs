using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.PropertyBinderBase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Reflection;

namespace Digitteck.JDConverter.PropertyBinders
{
    public class JDListPropertyBinder : IJDPropertyBinder
    {
        private JDPropertyBinderManager propertyBinder;
        private JDTokenLookup jTokenLookup;
        private JDConvert jDConvert;
        private JDTokenReader jTokenValueReader;

        public JDListPropertyBinder()
        {
            propertyBinder = new JDPropertyBinderManager();
            jTokenLookup = new JDTokenLookup();
            jDConvert = new JDConvert();
            jTokenValueReader = new JDTokenReader();
        }

        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            if (!propertyInfo.CanWrite) return true;

            var propertyObject = Activator.CreateInstance(propertyInfo.PropertyType) as IList;

            JArray matchingToken = jTokenLookup.FindJArray(propertyPath, parentJson);

            if (matchingToken != null && matchingToken.Type == JTokenType.Array)
            {
                foreach (var tokenChild in matchingToken.Children())
                {
                    object listItem = null;

                    if (tokenChild is JObject jObjectChild)
                    {
                        listItem = jDConvert.Deserialize(propertyInfo.PropertyType.GenericTypeArguments[0], jObjectChild);

                        if (listItem != null)
                        {
                            propertyObject.Add(listItem);
                        }
                    }
                    else if (tokenChild is JValue jValueChild)
                    {
                        if (propertyInfo.PropertyType.GenericTypeArguments.Length == 1)
                        {
                            listItem = jTokenValueReader.ReadValueFromJValue(propertyInfo.PropertyType.GenericTypeArguments[0], jValueChild);
                        }
                        if (propertyInfo.PropertyType.GenericTypeArguments.Length == 0)
                        {
                            listItem = jTokenValueReader.ReadValueFromJValue(typeof(object), jValueChild);
                        }

                        if (listItem != null)
                        {
                            propertyObject.Add(listItem);
                        }
                    }
                }

                if (propertyObject != null)
                {
                    propertyInfo.SetValue(parentModel, propertyObject);
                    return true;
                }
            }

            return false;
        }
    }
}
