using Digitteck.JDConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_13_SomeObj
    {

    }

    public class OM_13_WrongData
    {
        [JsonPropertyPath("wrongdata.nullobject")]
        public OM_13_SomeObj NullObject { get; set; }

        [JsonPropertyPath("wrongdata.undefinedint")]
        public int UndefinedInt { get; set; }

        [JsonPropertyPath("wrongdata.nullint")]
        public int NullInt { get; set; }
    }
}
