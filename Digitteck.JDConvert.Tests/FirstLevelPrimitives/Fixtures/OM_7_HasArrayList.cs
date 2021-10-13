using Digitteck.JDConverter.Attributes;
using System.Collections;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_7_HasArrayList
    {
        [JsonPropertyPath("data.values")]
        public ArrayList Values { get; set; }

        [JsonPropertyPath("data.names")]
        public ArrayList Names { get; set; }
    }
}
