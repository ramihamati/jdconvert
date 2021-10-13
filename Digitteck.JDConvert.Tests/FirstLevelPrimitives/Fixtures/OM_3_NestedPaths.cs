using Digitteck.JDConverter.Attributes;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_3_NestedPaths
    {
        [JsonPropertyPath("attributes.displayname")]
        public string DisplayName { get; set; }
        [JsonPropertyPath("attributes.metadata.filetype")]
        public string FileType { get; set; }

        [JsonPropertyPath("stringvalue")]
        public string StringValue { get; set; }
    }
}
