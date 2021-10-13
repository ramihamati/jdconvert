using Digitteck.JDConverter.Attributes;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_5_Attributes
    {
        [JsonPropertyPath("displayname")]
        public string DisplayName { get; set; }
        [JsonPropertyPath("metadata.filetype")]
        public string FileType { get; set; }
    }

    public class OM_5_ChildObject
    {
        [JsonPropertyPath("stringvalue")]
        public string StringValue { get; set; }

        [JsonPropertyPath("attributes")]
        public OM_5_Attributes ObjectAttributes { get; set; }
    }
}
