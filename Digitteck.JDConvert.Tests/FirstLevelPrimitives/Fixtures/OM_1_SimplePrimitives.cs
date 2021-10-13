using Digitteck.JDConverter.Attributes;
using System;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_1_SimplePrimitives
    {
        [JsonPropertyPath("datetimeoffset")]
        public DateTimeOffset DateTimeOffset { get; set; }
        
        [JsonPropertyPath("intvalue")]
        public int IntValue { get; set; }

        [JsonPropertyPath("intfromstring")]
        public int IntFromString { get; set; }

        [JsonPropertyPath("floatvalue")]
        public float FloatValue { get; set; }

        [JsonPropertyPath("doublevalue")]
        public double DoubleValue { get; set; }

        [JsonPropertyPath("boolvaluefromstring")]
        public bool BoolValueFromString { get; set; }

        [JsonPropertyPath("boolvalue")]
        public bool BoolValue { get; set; }

        [JsonPropertyPath("stringvalue")]
        public string StringValue { get; set; }

        [JsonPropertyPath("datetime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyPath("dateotherformat")]
        public DateTime dateotherformat { get; set; }
        
        [JsonPropertyPath("decimalvalue")]
        public decimal DecimalValue { get; set; }
    }
}
