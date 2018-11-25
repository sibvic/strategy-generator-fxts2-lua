namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    static class PrimitiveUtils
    {
        /// <summary>
        /// Makes string primitive
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IPrimitive MakePrimitive(this string str)
        {
            return new StringPrimitive(str);
        }
    }
}
