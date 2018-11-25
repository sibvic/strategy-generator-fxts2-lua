using System.Collections.Generic;

namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    interface IPrimitive
    {
        string ToCode();

        /// <summary>
        /// Get list of validations check. Used to check whether parameter is valid and can be used futher.
        /// </summary>
        /// <returns></returns>
        List<IPrimitive> GetValidationChecks();

        /// <summary>
        /// Get list of constants used in this primitive
        /// </summary>
        /// <returns></returns>
        List<ConstPrimitive> GetConstants();
    }
}
