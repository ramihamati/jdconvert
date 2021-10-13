using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.PropertyBinderBase;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Digitteck.JDConverter.PropertyBinders
{
    public class JDBasicPropertyBinder : IJDPropertyBinder
    {
        public bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson)
        {
            if (!propertyInfo.CanWrite)
            {
                //property is unwritable. Returning true to mark it as solved
                return true;
            }

            JDTokenReader tokenReader = new JDTokenReader();
            JDValueConverterManager jDValueConverterManager = new JDValueConverterManager();

            object propertyvalue = tokenReader.GetValueFor(propertyInfo.PropertyType, propertyPath, parentJson);

            propertyvalue = jDValueConverterManager.Convert(propertyInfo.PropertyType, propertyvalue);

            if (propertyvalue != null && propertyvalue.GetType().Equals(propertyInfo.PropertyType))
            {

                propertyInfo.SetValue(parentModel, propertyvalue);
                return true;
            }
            return false;
        }
    }
}
