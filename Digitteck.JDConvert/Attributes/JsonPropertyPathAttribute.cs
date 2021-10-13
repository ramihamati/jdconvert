using System;

namespace Digitteck.JDConverter.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class JsonPropertyPathAttribute : Attribute
    {
        public JsonPropertyPathAttribute(string path, string pathArguments = null)
        {
            Path = path;
            PathArguments = pathArguments;
        }

        public string Path { get; }
        public string PathArguments { get; }
    }
}
