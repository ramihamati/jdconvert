using System.Reflection;
using Digitteck.JDConverter.Attributes;
using Newtonsoft.Json.Linq;

namespace Digitteck.JDConverter.PropertyBinderBase
{
    public interface IJDPropertyBinder
    {
        bool SolveProperty(object parentModel, JsonPropertyPathAttribute propertyPath, PropertyInfo propertyInfo, JObject parentJson);
    }
}