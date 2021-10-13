using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class DoubleConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            bool parsed = Double.TryParse(sourceValue.ToString(), out double doubleInput);
            return parsed ? doubleInput : (double)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(double));
        }
    }
}
