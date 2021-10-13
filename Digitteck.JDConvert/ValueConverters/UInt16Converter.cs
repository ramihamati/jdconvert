using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class UInt16Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = ushort.TryParse(strInput, out ushort parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to UInt16");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (UInt16)int16Input;
            }
            else if (sourceValue is Int32 int32Input && int32Input <= UInt16.MaxValue)
            {
                return (UInt16)int32Input;
            }
            else if (sourceValue is Int64 int64Input && int64Input <= UInt16.MaxValue)
            {
                return (UInt16)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input && uint16Input <= UInt16.MaxValue)
            {
                return (UInt16)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input && uint32Input <= UInt16.MaxValue)
            {
                return (UInt16)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input && uint64Input <= (long)UInt16.MaxValue)
            {
                return (UInt16)uint64Input;
            }

            return (UInt16)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(UInt16));
        }
    }
}
