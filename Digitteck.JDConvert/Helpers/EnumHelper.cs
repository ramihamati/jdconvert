using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Digitteck.JDConverter.Helpers
{
    public static class EnumHelper
    {
        public static object GetEnumValue(Type propertyType, string propertyStrValue)
        {
            object propertyEnumValue = null;

            //we have the string value from json which will pe parsed to the enum value

            foreach (object enumvalue in Enum.GetValues(propertyType))
            {
                if (string.Equals(Enum.GetName(propertyType, enumvalue).Trim(), propertyStrValue.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    propertyEnumValue = enumvalue;
                    break;
                }
            }

            if (propertyEnumValue == null)
            {
                foreach (string fieldName in Enum.GetNames(propertyType))
                {
                    FieldInfo fieldInfo = propertyType.GetField(fieldName, BindingFlags.Public | BindingFlags.Static);

                    if (fieldInfo != null)
                    {
                        if (fieldInfo.GetCustomAttribute<EnumMemberAttribute>() is EnumMemberAttribute attribute)
                        {
                            if (string.Equals(attribute.Value.Trim(), propertyStrValue.Trim(), StringComparison.OrdinalIgnoreCase))
                            {
                                propertyEnumValue = fieldInfo.GetValue(propertyType);
                                break;
                            }
                        }
                    }
                }
            }

            return propertyEnumValue;
        }
    }
}
