using System;
using System.Linq;
using Microsoft;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Potions;
using NoxusBoss;
using HeavenlyArsenal;
using Luminance;
using Daybreak;

namespace CalamitySouls.Content.Bosses.Minions.Spider
{
    public class AuricSpider : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;

            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<MiracleBlight>()] = true;
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

        public override void AI()
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
