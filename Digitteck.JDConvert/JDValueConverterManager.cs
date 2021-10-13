using Digitteck.JDConverter.ValueConverters;
using System;
using System.Collections.Generic;

namespace Digitteck.JDConverter
{
    public class JDValueConverterManager
    {
        public JDValueConverterManager()
        {
            this.Converters = new List<IValueConvert>()
            {
                new BoolConverter(),
                new EnumValueConverter(),
                new Int16Converter(),
                new Int32Converter(),
                new Int64Converter(),
                new UInt16Converter(),
                new UInt32Converter(),
                new UInt64Converter(),
                new StringConverter(),
                new SingleConverter(),
                new DoubleConverter(),
                new DateTimeConverter(),
                new DecimalConverter()
            };
        }

        public List<IValueConvert> Converters { get; }

        public object Convert(Type targetType, object sourceValue)
        {
            foreach (var converter in this.Converters)
            {
                if (converter.ValidConverter(targetType))
                {
                    return converter.Convert(targetType, sourceValue);
                }
            }

            return sourceValue;
        }
    }
}
