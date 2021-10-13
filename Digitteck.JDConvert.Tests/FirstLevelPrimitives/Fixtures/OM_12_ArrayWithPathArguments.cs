using Digitteck.JDConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_12_Version
    {
        [JsonPropertyPath("type")]
        public FileVersionType FileVersionType { get; set; }

        [JsonPropertyPath("number")]
        public int VersionNumber { get; set; }
    }

    public class OM_12_ArrayWithPathArguments
    {
        [JsonPropertyPath("data.versions[{0}]", "{IndexOfIf(data.versions, type, updatedfile)}")]
        public OM_12_Version VersionAtIndex { get; set; }

        [JsonPropertyPath("data.versions[{0}]", "{IndexOfIf(data.versions, type, abc)}")]
        public OM_12_Version VersionAtNoIndex { get; set; }


        [JsonPropertyPath("data.versions[{0}].type", "{IndexOfIf(data.versions, type, updatedfile)}")]
        public FileVersionType VersionTypeAtIndex { get; set; }

        [JsonPropertyPath("data.versions[{0}].type", "{IndexOfIf(data.versions, type, abc)}")]
        public FileVersionType VersionTypeAtNoIndex { get; set; }
    }
}
