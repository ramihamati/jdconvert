using System.Runtime.Serialization;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public enum YesNo
    {
        NotDefined,

        [EnumMember(Value = "yes")]

        Yes,

        [EnumMember(Value = "no")]
        No
    }
}
