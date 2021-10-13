using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class DateTimeConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            bool parsed = DateTime.TryParse(sourceValue.ToString(), out DateTime dateTimeValue);

            return parsed ? dateTimeValue : default(DateTime);
        }

        public bool ValidConverter(Type targetType)
        {
            return (targetType.Equals(typeof(DateTime)));
        }
    }
}
