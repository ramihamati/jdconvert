using System.Runtime.Serialization;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public enum FileVersionType
    {
        NotDefined,

        [EnumMember(Value = "newfile")]
        NewFile,

        [EnumMember(Value = "updatedfile")]
        UpdateFile
    }
}
