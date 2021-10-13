using System.Runtime.InteropServices;

namespace Digitteck.JDConverter.DataStructures
{
    [StructLayout(LayoutKind.Explicit)]
    public struct BoolIntOverlapped
    {
        [FieldOffset(0)] private readonly bool BoolValue;

        [FieldOffset(0)] private readonly int IntValue;

        public BoolIntOverlapped(int value)
        {
            this.BoolValue = false; //pre initialize
            this.IntValue = value;
        }

        public int GetInt() => IntValue;
        public bool GetBool() => BoolValue;
    }
}
