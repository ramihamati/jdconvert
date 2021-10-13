using Digitteck.JDConverter.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{

    public class OM_6_Version
    {
        [JsonPropertyPath("type")]
        public FileVersionType VersionType { get; set; }

        [JsonPropertyPath("number")]
        public int VersionNumber { get; set; }
    }

    public class OM_6_ObjWithList
    {
        [JsonPropertyPath("versions")]
        public List<OM_6_Version> Versions { get; set; }
    }

    public class OM_6_ChildWithLists
    {
        [JsonPropertyPath("data")]
        public OM_6_ObjWithList OMWithLists { get; set; }
    }
}
