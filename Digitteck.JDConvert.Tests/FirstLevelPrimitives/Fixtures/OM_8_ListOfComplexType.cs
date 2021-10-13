using Digitteck.JDConverter.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_8_Version
    {
        [JsonPropertyPath("type")]
        public FileVersionType VersionType { get; set; }

        [JsonPropertyPath("number")]
        public int VersionNumber { get; set; }
    }

    public class OM_8_ListOfComplexType
    {
        [JsonPropertyPath("data.versions")]
        public List<OM_8_Version> Versions { get; set; }
    }
}
