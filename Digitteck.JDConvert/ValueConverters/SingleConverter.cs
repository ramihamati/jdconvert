using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public class SingleConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            bool parsed = Single.TryParse(sourceValue.ToString(), out float floatInput);

            return parsed ? floatInput : 0f;
        }

        public bool ValidConverter(Type targetType)
        {
            return (targetType.Equals(typeof(Single)));
        }
    }
}
