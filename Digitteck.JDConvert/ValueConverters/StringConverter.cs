using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class StringConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            return sourceValue?.ToString() ?? null;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(string));
        }
    }
}
