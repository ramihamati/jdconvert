using Digitteck.JDConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_11_Version
    {
        [JsonPropertyPath("type")]
        public FileVersionType FileVersionType { get; set; }

        [JsonPropertyPath("number")]
        public int VersionNumber { get; set; }
    }

    public class OM_11_ArrayValueAtIndex
    {
        [JsonPropertyPath("data.versions[1]")]
        public OM_11_Version VersionAtIndex { get; set; }

        [JsonPropertyPath("data.versions[10]")]
        public OM_11_Version VersionInexistingIndex { get; set; }

        [JsonPropertyPath("data.versions[-1]")]
        public OM_11_Version VersionNegativeIndex { get; set; }

        [JsonPropertyPath("data.versions[a]")]
        public OM_11_Version VersionInvalidIndex { get; set; }

        [JsonPropertyPath("data.versions[]")]
        public OM_11_Version VersionInvalidIndex2 { get; set; }


        [JsonPropertyPath("data.versions[1].type")]
        public FileVersionType VersionTypeAtIndex { get; set; }

        [JsonPropertyPath("data.versions[10].type")]
        public FileVersionType VersionTypeInexistingIndex { get; set; }

        [JsonPropertyPath("data.versions[-1].type")]
        public FileVersionType VersionTypeNegativeIndex { get; set; }

        [JsonPropertyPath("data.versions[a].type")]
        public FileVersionType  VersionTypeInvalidIndex { get; set; }

        [JsonPropertyPath("data.versions[].type")]
        public FileVersionType VersionTypeInvalidIndex2 { get; set; }
    }
}
