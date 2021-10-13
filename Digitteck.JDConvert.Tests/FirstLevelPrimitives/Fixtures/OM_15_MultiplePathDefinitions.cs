using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_15_MultiplePathDefinitions
    {
        [JsonPropertyPath("hintvalue")]
        [JsonPropertyPath("intvalue")]
        [JsonPropertyPath("mintvalue")]
        public int IntValue{ get; set; }
    }
}
