using Digitteck.JDConverter.Attributes;
using System;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_2_EnumsBasicStructs
    {
        [JsonPropertyPath("enumValue")]
        public YesNo YesNo { get; set; }

        [JsonPropertyPath("enumValueWithNoAttribute")]
        public OkNotOk OkNotOk { get; set; }

        [JsonPropertyPath("datetimeoffset")]
        public DateTimeOffset   DateTimeOffset{ get; set; }

        [JsonPropertyPath("datetime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyPath("dateonly")]
        public DateTime DateOnly { get; set; }

        [JsonPropertyPath("guidvalue")]
        public Guid Guid { get; set; }
    }
}
