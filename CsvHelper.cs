using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Lab1
{
    public static class CsvHelper
    {
        public static string Join(params object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                var strValue = values[i].ToString();
                if (strValue != null && strValue.Contains(','))
                {
                    values[i] = strValue.Insert(strValue.Length, "\"")
                                        .Insert(0, "\"");
                }
            }
            return string.Join(',', values);
        }

        public static List<string> Split(string csvLine)
        {
            var lines = new List<string>();
            var regexGlobal = new Regex("(\".*?\",)|(['-z]*?,)|(\"?.*?\"?$)");
            var matches = regexGlobal.Matches(csvLine);
            for (int i = 0; i < matches.Count - 1; i++)
            {
                var match = matches[i].Value.Replace("\"", "");
                if (match[^1] == ',')
                {
                    match = match.Remove(match.Length - 1);
                }
                lines.Add(match);
            }
            return lines;
        }
    }
}
