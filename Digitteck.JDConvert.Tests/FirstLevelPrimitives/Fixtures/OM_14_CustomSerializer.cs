using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class StringValueObject
    {
        public string Value { get; private set; }
        private StringValueObject() { }

        public static StringValueObject Create(string value)
        {
            return new StringValueObject { Value = value };
        }
    }

    public class OM_14_CustomSerializer : IJDDeserializer
    {
        public object Convert(JToken jToken)
        {
            string value = jToken.Value<string>();

            return StringValueObject.Create(value ?? "");
        }
    }

    public class OM_14_PropertyWithSerializer
    {
        [JsonPropertyPath("valueobject")]
        [JDDeserializer(typeof(OM_14_CustomSerializer))]
        public StringValueObject StringValueObject { get; set; }
    }
}
