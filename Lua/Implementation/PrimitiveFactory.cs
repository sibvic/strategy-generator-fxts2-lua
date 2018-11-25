namespace ProfitRobots.StrategyGenerator.Lua.Implementation
{
    /// <summary>
    /// Primitives factory
    /// </summary>
    class PrimitiveFactory
    {
        bool _useConst = false;

        /// <summary>
        /// Makes constant from primitive
        /// </summary>
        /// <param name="primitive">Primitive to wrap</param>
        /// <param name="force">Forces creation of the const primitive (event if the parameters set to not creating consts)</param>
        /// <returns></returns>
        public IPrimitive MakeConst(IPrimitive primitive, bool force = false)
        {
            if (_useConst || force)
                return new ConstPrimitive(primitive);
            return primitive;
        }

        /// <summary>
        /// Creates comparison ~- nil primitive
        /// </summary>
        /// <param name="primitive">Primitive</param>
        /// <returns>Is not null primitive</returns>
        public IPrimitive MakeIsNotNull(IPrimitive primitive)
        {
            return new NotEqualPrimitive(primitive, "nil".MakePrimitive());
        }
    }
}
