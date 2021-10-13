using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class UInt64Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = ulong.TryParse(strInput, out ulong parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to UInt64");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (UInt64)int16Input;
            }
            else if (sourceValue is Int32 int32Input)
            {
                return (UInt64)int32Input;
            }
            else if (sourceValue is Int64 int64Input)
            {
                return (UInt64)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input)
            {
                return (UInt64)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input)
            {
                return (UInt64)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input)
            {
                return (UInt64)uint64Input;
            }

            return (UInt64)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(UInt64));
        }
    }
}
