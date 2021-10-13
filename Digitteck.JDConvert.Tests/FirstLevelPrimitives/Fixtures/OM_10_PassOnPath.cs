using Digitteck.JDConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_10_Values
    {
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
    }

    public class OM_10_PassOnPath
    {
        [JsonPropertyPath("")]
        public OM_10_Values Values { get; set; }
    }
}
