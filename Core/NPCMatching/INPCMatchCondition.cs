namespace CalamitySouls.Core.NPCMatching
{
    public interface INPCMatchCondition
    {
        bool Satisfies(int type);
    }
}
