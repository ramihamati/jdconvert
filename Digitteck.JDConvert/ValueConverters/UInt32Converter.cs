using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class UInt32Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = uint.TryParse(strInput, out uint parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to UInt32");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (UInt32)int16Input;
            }
            else if (sourceValue is Int32 int32Input)
            {
                return (UInt32)int32Input;
            }
            else if (sourceValue is Int64 int64Input && int64Input <= UInt32.MaxValue)
            {
                return (UInt32)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input)
            {
                return (UInt32)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input && uint32Input <= UInt32.MaxValue)
            {
                return (UInt32)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input && uint64Input <= (long)UInt32.MaxValue)
            {
                return (UInt32)uint64Input;
            }

            return (UInt32)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(UInt32));
        }
    }
}
