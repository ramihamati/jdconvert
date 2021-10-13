using Digitteck.JDConverter.DataStructures;
using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class BoolConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is bool)
            {
                return sourceValue;
            }
            if (sourceValue is string stringValue)
            {
                if (stringValue.ToLower().Trim() == "false")
                {
                    return false;
                }
                else if (stringValue.ToLower().Trim() == "true")
                {
                    return true;
                }
                else if (int.TryParse(stringValue, out int intParsed))
                {
                    BoolIntOverlapped boolIntOverlapped = new BoolIntOverlapped(intParsed);
                    return boolIntOverlapped.GetBool();
                }
            }
            if (sourceValue is int intValue)
            {
                //0 is false
                return 0 != intValue;
            }

            return null;
        }

        public bool ValidConverter(Type targetType)
        {
            return (targetType.Equals(typeof(bool)));
        }
    }
}
