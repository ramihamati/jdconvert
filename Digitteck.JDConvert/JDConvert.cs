using Digitteck.JDConverter.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Digitteck.JDConverter
{
    public class JDConvert
    {
        private JDPropertyBinderManager propertyBinder;

        public JDConvert()
        {
            propertyBinder = new JDPropertyBinderManager();
        }

        public static T Deserialize<T>(JObject jObject) where T : class
        {
            return (new JDConvert()).DeserializeObject<T>(jObject);
        }

        public object Deserialize(Type modelType, JObject jObject)
        {
            object newObject = Activator.CreateInstance(modelType);

            foreach (var pInfo in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pInfo.GetCustomAttributes<JsonPropertyPathAttribute>() is IEnumerable<JsonPropertyPathAttribute> attributes)
                {
                    //a property can have multiple attributes. Will stop at the first one solved
                    foreach(var attr in attributes)
                    {
                        if (attr != null)
                        {
                            bool solved = propertyBinder.SolveProperty(newObject, attr, pInfo, jObject);

                            if (solved) break;
                        }
                    }
                }
            }

            return newObject;
        }

        public T DeserializeObject<T>(JObject jObject) where T : class
        {
            return Deserialize(typeof(T), jObject) as T;
        }
    }
}
