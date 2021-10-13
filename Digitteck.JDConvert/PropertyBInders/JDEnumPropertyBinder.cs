using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.Helpers;
using Digitteck.JDConverter.PropertyBinderBase;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Digitteck.JDConverter.PropertyBinders
{ 
    public class JDEnumPropertyBinder : IJDPropertyBinder
    {
        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            if (!propertyInfo.CanWrite) return true;

            JDTokenReader tokenReader = new JDTokenReader();

            object propertyvalue = tokenReader.GetValueFor(propertyInfo.PropertyType, propertyPath, parentJson);

            if (propertyvalue != null && propertyvalue.GetType().Equals(propertyInfo.PropertyType))
            {
                propertyInfo.SetValue(parentModel, propertyvalue);
                return true;
            }

            return false;
        }
    }
}
