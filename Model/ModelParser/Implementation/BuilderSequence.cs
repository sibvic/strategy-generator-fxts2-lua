using System.Collections.Generic;
using ProfitRobots.StrategyGenerator.ModelParser.Implementation;

namespace ProfitRobots.StrategyGenerator.ModelParser
{
    class BuilderSequence : IBuilder
    {
        List<IBuilder> _builders = new List<IBuilder>();
        int _index = 0;

        public BuilderSequence Add(IBuilder builder)
        {
            _builders.Add(builder);
            return this;
        }

        bool Next() => _builders.Count > ++_index;
        IBuilder Current => _builders[_index];

        public bool TryAddWord(Token word)
        {
            if (!Current.TryAddWord(word))
            {
                if (!Next() || !Current.TryAddWord(word))
                    throw new InvalidSemanticItemException(null, word.Data);
            }
            return true;
        }
    }
}
