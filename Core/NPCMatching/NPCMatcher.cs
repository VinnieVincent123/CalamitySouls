using System.Collections.Generic;
using CalamitySouls.Core.NPCMatching.Conditions;

namespace CalamitySouls.Core.NPCMatching
{
    public class NPCMatcher
    {
        public List<INPCMatchCondition> Conditions;

        public NPCMatcher()
        {
            Conditions =
            [
                new MatchEverythingCondition()
            ];
        }

        public NPCMatcher MatchType(int type)
        {
            Conditions.Add(new MatchTypeCondition(type));
            return this;
        }

        public NPCMatcher MatchTypeRange(params int[] types)
        {
            Conditions.Add(new MatchTypeRangeCondition(types));
            return this;
        }

        public bool Satisfies(int type) => Conditions.TrueForAll(condition => condition.Satisfies(type));
    }
}

