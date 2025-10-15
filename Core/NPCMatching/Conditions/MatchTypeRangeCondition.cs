using System.Collections.Generic;
using System.Linq;
using CalamitySouls.Core.NPCMatching;

namespace CalamitySouls.Core.NPCMatching.Conditions
{
    public class MatchTypeRangeCondition : INPCMatchCondition
    {
        public int[] Types;

        public MatchTypeRangeCondition(IEnumerable<int> types)
        {
            Types = types.ToArray();
        }

        public MatchTypeRangeCondition(params int[] types)
        {
            Types = types;
        }

        public bool Satisfies(int type) => Types.Contains(type);
    }
}

