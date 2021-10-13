using System;

namespace Digitteck.JDConverter.ValueConverters
{
    public sealed class Int32Converter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is string strInput)
            {
                bool parseRes = int.TryParse(strInput, out int parsed);
                if (!parseRes)
                {
                    throw new Exception($"Error converting {strInput} to Int32");
                }

                return parsed;
            }
            else if (sourceValue is Int16 int16Input)
            {
                return (int)int16Input;
            }
            else if (sourceValue is Int32 int32Input)
            {
                return (int)int32Input;
            }
            else if (sourceValue is Int64 int64Input && int64Input <= int.MaxValue)
            {
                return (int)int64Input;
            }
            else if (sourceValue is UInt16 uint16Input)
            {
                return (int)uint16Input;
            }
            else if (sourceValue is UInt32 uint32Input && uint32Input <= int.MaxValue)
            {
                return (int)uint32Input;
            }
            else if (sourceValue is UInt64 uint64Input && uint64Input <= int.MaxValue)
            {
                return (int)uint64Input;
            }

            return 0;
        }

        public bool ValidConverter(Type targetType)
        {
            return targetType.Equals(typeof(Int32));
        }
    }
}
