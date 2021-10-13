using Digitteck.JDConverter.Attributes;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace Digitteck.JDConverter
{
    public class JDTokenLookup
    {
        private readonly JDPathArgumentsSolver pathArgumentsSolver;

        public JDTokenLookup()
        {
            this.pathArgumentsSolver = new JDPathArgumentsSolver();
        }

        public JObject FindJObject(JsonPropertyPathAttribute key, JObject jObject)
        {
            string solvedPath = pathArgumentsSolver.SolvePathWithPathArguments(key, jObject);

            if (solvedPath == "") return jObject;

            string[] parts = solvedPath.Split('.');

            return FindTokenType<JObject>(jObject, parts);
        }

        public JValue FindJValue(JsonPropertyPathAttribute key, JObject jObject)
        {
            string rawPropertyPath = pathArgumentsSolver.SolvePathWithPathArguments(key, jObject);

            string[] parts = rawPropertyPath.Split('.');

            return FindTokenType<JValue>(jObject, parts);
        }

        public JToken FindJToken(JsonPropertyPathAttribute key, JObject jObject)
        {
            string rawPropertyPath = pathArgumentsSolver.SolvePathWithPathArguments(key, jObject);

            if (rawPropertyPath == "") return jObject;

            string[] parts = rawPropertyPath.Split('.');

            return FindTokenType<JToken>(jObject, parts);
        }

        public JArray FindJArray(JsonPropertyPathAttribute key, JObject jObject)
        {
            string rawPropertyPath = pathArgumentsSolver.SolvePathWithPathArguments(key, jObject);

            string[] parts = rawPropertyPath.Split('.');

            return FindTokenType<JArray>(jObject, parts);
        }

        private T FindTokenType<T>(JObject jObject, string[] keyparts, int partIndex = 0) where T : JToken
        {
            if (IsArrayIndexSymbol(keyparts[partIndex]))
            {
                return GetArrayItemAtSymbolIndex<T>(jObject, keyparts, partIndex);
            }
            else
            {
                return GetTokenType<T>(jObject, keyparts, partIndex);
            }
        }

        private T GetArrayItemAtSymbolIndex<T>(JObject jObject, string[] keyparts, int partIndex) where T : JToken
        {
            int index = GetArrayIndex(keyparts[partIndex]);
            string collectionName = GetCollectionName(keyparts[partIndex]);

            bool hasToken1 = jObject.TryGetValue(collectionName, out JToken partToken1);

            if (hasToken1 && partToken1 != null && partToken1.Type == JTokenType.Array)
            {
                JArray jArray = (JArray)partToken1;

                if (partToken1.Children().Count() > index)
                {
                    JToken child = jArray.Children().ToList()[index];

                    if (child == null) return null;

                    if (partIndex == keyparts.Length - 1 && child != null)
                    {
                        return child as T;
                    }
                    else if (child is JObject jChildObject)
                    {
                        return FindTokenType<T>(jChildObject, keyparts, partIndex + 1);
                    }
                }
            }

            return null;
        }

        private T GetTokenType<T>(JObject jObject, string[] keyparts, int partIndex) where T : JToken
        {
            bool hasToken = jObject.TryGetValue(keyparts[partIndex], out JToken partToken);

            if (hasToken)
            {
                if (partIndex == keyparts.Length - 1)
                {
                    if (partToken is T jvalue)
                    {
                        return jvalue;
                    }
                }
                else if (partIndex < (keyparts.Length - 1))
                {
                    if (partToken is JObject jChildObject)
                    {
                        return FindTokenType<T>(jChildObject, keyparts, partIndex + 1);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        private bool IsArrayIndexSymbol(string v)
        {
            return Regex.IsMatch(v, @"\[[0-9]+\]$");
        }

        private int GetArrayIndex(string v)
        {
            Regex regex = new Regex(@"(\[(?<index>[0-9]+)\])$");

            var match = regex.Match(v);

            if (match.Success)
            {
                bool parsed = int.TryParse(match.Groups["index"].Value, out int index);

                if (parsed)
                {
                    return index;
                }

                return -1;
            }

            return -1;
        }

        private string GetCollectionName(string v)
        {
            Regex regex = new Regex(@"(?<part>.*?)\[[0-9]+\]$");

            var match = regex.Match(v);

            if (match.Success)
            {
                return match.Groups["part"].Value;
            }

            return null;
        }
    }
}
