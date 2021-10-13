using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.PropertyBinders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Reflection;

namespace Digitteck.JDConverter
{
    public class JDPropertyBinderManager
    {
        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            //if it has a custom serializer defined by an attribute - will use this serializer
            JDPropertyAttributeBinder jDPropertyAttributeBinder = new JDPropertyAttributeBinder();
            jDPropertyAttributeBinder.SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);

            if (jDPropertyAttributeBinder.ValueSolved)
            {
                return true;
            }

            if (propertyInfo.PropertyType == typeof(Int16) || propertyInfo.PropertyType == typeof(Int32)
                || propertyInfo.PropertyType == typeof(Int64) || propertyInfo.PropertyType == typeof(UInt16)
                || propertyInfo.PropertyType == typeof(UInt32) || propertyInfo.PropertyType == typeof(UInt64)
                || propertyInfo.PropertyType == typeof(float) || propertyInfo.PropertyType == typeof(double) 
                || propertyInfo.PropertyType == typeof(decimal)
                || propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(String)
                || propertyInfo.PropertyType == typeof(DateTimeOffset) || propertyInfo.PropertyType == typeof(DateTime))
            {
                return (new JDBasicPropertyBinder()).SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);

            }

            if (propertyInfo.PropertyType.GetInterface(nameof(IList)) != null)
            {
                //list
                return (new JDListPropertyBinder()).SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);
            }
            if (propertyInfo.PropertyType.IsClass)
            {
                //class
                return (new JDClassPropertyBinder()).SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);
            }
            if (propertyInfo.PropertyType.IsValueType && !propertyInfo.PropertyType.IsEnum && !propertyInfo.PropertyType.IsPrimitive)
            {
                //struct
                return (new JDClassPropertyBinder()).SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);
            }
            if (propertyInfo.PropertyType.IsEnum)
            {
                //enum
                return (new JDEnumPropertyBinder()).SolveProperty(parentModel, propertyPath, propertyInfo, parentJson);
            }

            Type propertyType = propertyInfo.PropertyType;

            JDTokenReader reader = new JDTokenReader();

            object propertyvalue = reader.GetValueFor(propertyType, propertyPath, parentJson);

            if (propertyvalue != null)
            {
                propertyInfo.SetValue(parentModel, propertyvalue);
                return true;
            }

            return false;
        }

        internal bool SolvePropertyWithDirectValue(object parentModel, JsonPropertyPathAttribute attr, PropertyInfo pInfo, object directClassValue)
        {
            /*
               -- if a json is like:
               "attributes": {
                    "displayname": "myfile",
             
                -- and the string is passed to the other class directly. The pInfo here is the DisplayName string property.
                public class OM_16_SendValueToClass
                {
                    [JsonPropertyPath("attributes.displayname")]
                    public OM_16_DisplayName DisplayName { get; set; }
                }

                public class OM_16_DisplayName
                {
                    [JsonPropertyPath("")]
                    public string DisplayName { get; set; }
                }
             */

            if (directClassValue == null) return false;

            if (!string.IsNullOrWhiteSpace(attr.Path)) return false;

            if (pInfo.PropertyType.Equals(directClassValue.GetType()))
            {
                pInfo.SetValue(parentModel, directClassValue);
                return true;
            }

            return false;
        }
    }
}
