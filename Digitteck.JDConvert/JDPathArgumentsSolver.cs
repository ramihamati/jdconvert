using Digitteck.JDConverter.Attributes;
using Digitteck.JDConverter.Extensions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Digitteck.JDConverter
{
    public sealed class JDPathArgumentsSolver
    {
        public string SolvePathWithPathArguments(JsonPropertyPathAttribute key, JObject jObject)
        {
            string rawPropertyPath = key.Path;

            if (!string.IsNullOrWhiteSpace(key.PathArguments))
            {
                var evaluatedParts = SolveEvaluate(key.PathArguments.RemoveWhitespaces(), jObject);

                foreach (var kvp in evaluatedParts)
                {
                    rawPropertyPath = rawPropertyPath.Replace("{" + kvp.Key.ToString() + "}", kvp.Value);
                }
            }

            return rawPropertyPath;
        }

        private Dictionary<int, string> SolveEvaluate(string evaluate, JObject jObject)
        {
            Dictionary<int, string> evaluated = new Dictionary<int, string>();

            //get groups
            Regex regex = new Regex(@"{(.*?)+}");

            var match = regex.Match(evaluate);
            int index = 0;

            if (match.Success)
            {
                foreach (var groupItem in match.Groups)
                {
                    if (groupItem is Group group)
                    {
                        if (string.IsNullOrWhiteSpace(group.Value)) continue;

                        string evaluatedPartValue = SolveEvaluateItem(group.Value, jObject);
                        evaluated.Add(index, evaluatedPartValue);
                        index++;
                    }
                }
            }

            return evaluated;
        }

        private string SolveEvaluateItem(string value, JObject jObject)
        {
            int fnIndex = value.IndexOf("(");

            string fnBody = value.Substring(fnIndex + 1, value.Length - fnIndex - 3);

            string[] fnArguments = fnBody.Split(',');

            string fnName = value.Substring(1, fnIndex - 1);

            if (fnName == "IndexOfIf")
            {
                string arrPath = fnArguments[0];
                string arrItemPath = fnArguments[1];
                string eqCondValue = fnArguments[2];

                JToken dataToken = GetTokenFromPath(jObject, arrPath.Split('.'));

                if (dataToken.Type == JTokenType.Array)
                {
                    int arrIndex = 0;

                    foreach (var item in dataToken.Children())
                    {
                        JToken dataArrToken = GetTokenFromPath((JObject)item, arrItemPath.Split('.'));

                        if (dataArrToken != null)
                        {
                            if (dataArrToken.Value<string>() == eqCondValue)
                            {
                                return arrIndex.ToString();
                            }
                        }

                        arrIndex++;
                    }
                }
            }

            return string.Empty;
        }

        private JToken GetTokenFromPath(JObject jObject, string[] jsonPathParts, int partIndex = 0)
        {
            bool hasToken = jObject.TryGetValue(jsonPathParts[partIndex], out JToken partToken);

            if (hasToken && partToken != null)
            {
                if (partIndex == jsonPathParts.Length - 1)
                {
                    return partToken;
                }
                else
                {
                    if (partToken is JObject jChildObject)
                    {
                        return GetTokenFromPath(jChildObject, jsonPathParts, partIndex + 1);
                    }
                }
            }

            return null;
        }
    }
}
