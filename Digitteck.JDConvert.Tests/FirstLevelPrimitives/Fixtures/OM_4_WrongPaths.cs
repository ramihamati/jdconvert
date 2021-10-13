using Digitteck.JDConverter.Attributes;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_4_WrongPaths
    {
        [JsonPropertyPath("abc")]
        public string StringValue { get; set; }
    }
}
