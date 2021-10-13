using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.PropertyBinderBase;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Digitteck.JDConverter.PropertyBinders
{
    public class JDClassPropertyBinder : IJDPropertyBinder
    {
        private JDPropertyBinderManager propertyBinder;
        private JDTokenLookup jTokenLookup;

        public JDTokenReader JTokenReader { get; }

        public JDClassPropertyBinder()
        {
            propertyBinder = new JDPropertyBinderManager();
            jTokenLookup = new JDTokenLookup();
            JTokenReader = new JDTokenReader();
        }

        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            if (!propertyInfo.CanWrite) return true;

            //get an object with all properties
            JObject tokenForProperty = jTokenLookup.FindJObject(propertyPath, parentJson);

            if (tokenForProperty != null)
            {
                object propertyValueModel = Activator.CreateInstance(propertyInfo.PropertyType);

                foreach (var pInfo in propertyInfo.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    SetPropertyWithAJObjectSource(propertyValueModel, tokenForProperty, pInfo);
                }

                if (propertyValueModel != null)
                {
                    propertyInfo.SetValue(parentModel, propertyValueModel);
                    return true;
                }
            }

            //a value that is composed using a serializer (GUID). The model would have a GUID property (class) the json woudl have a string value
            //therefore there is no jobject but a direct value. (A JObject contains subproperties for a class)
            JValue jValue = jTokenLookup.FindJValue(propertyPath, parentJson);

            if(jValue != null)
            {
                object directClassValue = JTokenReader.ReadValueFromJValue(propertyInfo.PropertyType, jValue);

                if (directClassValue != null && directClassValue.GetType().Equals(propertyInfo.PropertyType))
                {
                    object propertyValueModel = Activator.CreateInstance(propertyInfo.PropertyType);

                    if (propertyValueModel != null)
                    {
                        propertyInfo.SetValue(parentModel, directClassValue);
                        return true;
                    }
                }
                else
                {
                    if (directClassValue!= null)
                    {
                        object propertyValueModel = Activator.CreateInstance(propertyInfo.PropertyType);

                        foreach (var pInfo in propertyInfo.PropertyType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                        {
                            SetPropertyWithADirectValue(propertyValueModel, directClassValue, pInfo);
                        }

                        if (propertyValueModel != null)
                        {
                            propertyInfo.SetValue(parentModel, propertyValueModel);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void SetPropertyWithAJObjectSource(object propertyValueModel, JObject tokenForProperty, PropertyInfo pInfo)
        {
            if (pInfo.GetCustomAttributes<JsonPropertyPathAttribute>() is IEnumerable<JsonPropertyPathAttribute> attributes)
            {
                //a property can have multiple attributes. Will stop at the first one solved
                foreach (var attr in attributes)
                {
                    if (attr != null)
                    {
                        bool solved = propertyBinder.SolveProperty(propertyValueModel, attr, pInfo, tokenForProperty);

                        if (solved) break;
                    }
                }

            }
        }

        private void SetPropertyWithADirectValue(object propertyValueModel, object directClassValue, PropertyInfo pInfo)
        {
            if (pInfo.GetCustomAttributes<JsonPropertyPathAttribute>() is IEnumerable<JsonPropertyPathAttribute> attributes)
            {
                //a property can have multiple attributes. Will stop at the first one solved
                foreach (var attr in attributes)
                {
                    if (attr != null)
                    {
                        bool solved = propertyBinder.SolvePropertyWithDirectValue(propertyValueModel, attr, pInfo, directClassValue);

                        if (solved) break;
                    }
                }

            }
        }
    }
}
