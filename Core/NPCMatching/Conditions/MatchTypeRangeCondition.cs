using System.Collections.Generic;
using System.Linq;

namespace FargowiltasSouls.Core.NPCMatching.Conditions
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

