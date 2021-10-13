using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.SerializerBase;
using Newtonsoft.Json.Linq;
using System;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_16_SendValueToClass
    {
        [JsonPropertyPath("attributes.displayname")]
        public OM_16_DisplayName DisplayName { get; set; }
    }

    public class OM_16_DisplayName
    {
        [JsonPropertyPath("")]
        public string DisplayName { get; set; }
    }
}
