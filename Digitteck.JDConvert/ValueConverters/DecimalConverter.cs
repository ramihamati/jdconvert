using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class DecimalConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            bool parsed = Decimal.TryParse(sourceValue.ToString(), out decimal decimalOutput);
            return parsed ? decimalOutput : (decimal)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(decimal));
        }
    }
}
