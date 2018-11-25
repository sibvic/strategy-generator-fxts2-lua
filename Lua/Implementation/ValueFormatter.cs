using System.Collections.Generic;
using System.Globalization;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Replaces parameters with it's values accodring to the specified formatting.
    /// </summary>
    public static class ValueFormatter
    {
        public static string ReplaceValues(string str, IEnumerable<string> valuesToReplace, string format)
        {
            foreach (var stream in valuesToReplace)
            {
                var streamValue = string.Format(format, stream.Substring(1, stream.Length - 2));
                str = str.Replace(stream, streamValue);
            }
            return str;
        }

        public static string ToLuaDoubleString(this double value)
        {
            return value.ToString("0.0#", CultureInfo.InvariantCulture);
        }
    }
}
