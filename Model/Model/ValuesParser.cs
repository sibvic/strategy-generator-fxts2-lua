using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Model
{
    public static class ValuesParser
    {
        public static IEnumerable<string> GetStreams(string value)
        {
            return GetTemplates(value, '[', ']');
        }

        public static IEnumerable<string> GetParams(string value)
        {
            return GetTemplates(value, '{', '}');
        }

        private static IEnumerable<string> GetTemplates(string value, char startChar, char endChar)
        {
            var streams = new List<string>();
            var index = value.IndexOf(startChar);
            while (index != -1)
            {
                var endIndex = value.IndexOf(endChar, index);
                streams.Add(value.Substring(index, endIndex - index + 1));
                value = value.Substring(endIndex + 1);
                index = value.IndexOf(startChar);
            }
            return streams;
        }

    }
}
