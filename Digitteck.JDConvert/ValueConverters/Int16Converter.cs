using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class Int16Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = short.TryParse(strInput, out short parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to Int16");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (short)int16Input;
            }
            else if (sourceValue is Int32 int32Input && int32Input <= short.MaxValue)
            {
                return (short)int32Input;
            }
            else if (sourceValue is Int64 int64Input && int64Input <= short.MaxValue)
            {
                return (short)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input && uint16Input <= short.MaxValue)
            {
                return (short)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input && uint32Input <= short.MaxValue)
            {
                return (short)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input && uint64Input <= (long)short.MaxValue)
            {
                return (short)uint64Input;
            }

            return (short)0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(Int16));
        }
    }
}
