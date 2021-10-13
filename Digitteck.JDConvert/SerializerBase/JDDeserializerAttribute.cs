using System;

namespace Digitteck.JDConverter.SerializerBase
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class JDDeserializerAttribute : Attribute
    {
        public JDDeserializerAttribute(Type serializer)
        {
            Serializer = serializer;
        }

        public Type Serializer { get; }
    }
}
