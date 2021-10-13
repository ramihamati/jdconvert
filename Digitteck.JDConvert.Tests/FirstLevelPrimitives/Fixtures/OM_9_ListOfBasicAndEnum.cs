using Digitteck.JDConverter.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures
{
    public class OM_9_ListOfBasicAndEnum
    {
        [JsonPropertyPath("data.values")]
        public List<int> Values { get; set; }

        [JsonPropertyPath("data.values")]
        public List<object> ObjValues { get; set; }


        [JsonPropertyPath("data.names")]
        public List<string> Names { get; set; }

        [JsonPropertyPath("data.filetypes")]
        public List<FileVersionType> FileTypes { get; set; }

        [JsonPropertyPath("data.guids")]
        public List<Guid> Guids { get; set; }
    }
}
