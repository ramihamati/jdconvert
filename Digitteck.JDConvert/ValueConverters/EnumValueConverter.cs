using Digitteck.JDConverter.Helpers;
using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class EnumValueConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string stringValue)
            {
                return EnumHelper.GetEnumValue(targetType, stringValue);
            }

            if (sourceValue.GetType().Equals(targetType))
            {
                return sourceValue;
            }

            return null;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.IsEnum;
        }
    }
}
