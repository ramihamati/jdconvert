using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.ValueConverters
{
    public interface IValueConvert
    {
        bool ValidConverter(Type targetType);
        object Convert(Type targetType, object sourceValue);
    }
}
