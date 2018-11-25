using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ProfitRobots.StrategyGenerator.Model.ModelParser
{
    static class Util
    {
        internal static string ReplaceParameter(string parameter)
        {
            if (parameter == null)
            {
                return null;
            }
            if (parameter.StartsWith("{") && parameter.EndsWith("}"))
            {
                return $"instance.parameters.{parameter.Substring(1, parameter.Length - 2)}";
            }
            return parameter;
        }

        public static string GetDisplayName<T>(this T enumValue) where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Argument must be of type Enum");
            try
            {
                return enumValue.GetType() // GetType causes exception if DisplayAttribute.Name is not set
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>()
                                .GetName();
            }
            catch // If there's no DisplayAttribute.Name set, just return the ToString value
            {
                return enumValue.ToString();
            }
        }
    }
}
