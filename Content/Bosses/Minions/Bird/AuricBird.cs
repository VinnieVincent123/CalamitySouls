

namespace CalamitySouls.Content.Bosses.Minions.AuricBird
{
    public class AuricBird : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;

            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<MiracleBlight>()] = true;

            this.ExcludeFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.height = 500;
            NPC.width = 300;
            NPC.lifeMax = 1800000;
            NPC.damage = 0;
            NPC.defense = 667;
            NPC.Calamity().DR = 0.6f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;

        }

        public override AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            Vector2 toPlayer = player.Center - NPC.Center;
            NPC.velocity = toPlayer * 0.01f;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<OmegaHealingPotion>();
        }
    }
}