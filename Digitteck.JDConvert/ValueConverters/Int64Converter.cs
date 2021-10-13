using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class Int64Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = long.TryParse(strInput, out long parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to Int64");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (long)int16Input;
            }
            else if (sourceValue is Int32 int32Input)
            {
                return (long)int32Input;
            }
            else if (sourceValue is Int64 int64Input)
            {
                return (long)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input)
            {
                return (long)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input)
            {
                return (long)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input && uint64Input <= long.MaxValue)
            {
                return uint64Input;
            }

            return 0L;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(Int64));
        }
    }
}
