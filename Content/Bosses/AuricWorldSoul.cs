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


namespace CalamitySouls.Content.Bosses
{
    public class AuricWorldSoul : ModNPC
    {
        private bool minionsSpawned = false;
        private bool transformed = false;
        private int phase = 1;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            Main.npcFrameCount[Type] = 1;

            NPCID.Sets.SpecificDebuffImmunity[Type][ModContent.BuffType<MiracleBlight>()] = true;

            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Position = new Vector2(16 * 4, 16 * 9),
                PortraitPositionXOverride = 16,
                PortraitPositionYOverride = 16 * 7
            });
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange([
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.CalamitySouls.Bestiary.AuricWorldSoul")
            ]);
        }
        public override void SetDefaults()
        {
            NPC.height = 5000;
            NPC.width = 3000;
            NPC.lifeMax = 4200000;
            NPC.damage = 0;
            NPC.defense = 897;
            NPC.Calamity().DR = 0.6f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.boss = true;

        }

        public override AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            Vector2 toPlayer = player.Center - NPC.Center;
            NPC.velocity = toPlayer * 0.01f;

            if (phase == 1)
            {
                HandlePhaseOne();
            }
            else if (phase == 2)
            {
                HandlePhaseTwo();
            }
        }

        private void HandlePhaseOne()
        {
            if (!minionsSpawned)
            {
                SpawnPhaseOneMinions();
                minionsSpawned = true;

                NPC.dontTakeDamage = true;
            }

            int[] minionTypes =
            {
                ModContent.NPCType<AuricBird>(),
                ModContent.NPCType<AuricSpider>(),
                ModContent.NPCType<AuricHorse>(),
                ModContent.NPCType<AuricSwine>(),
                ModContent.NPCType<AuricWorm>()
            };

            bool anyAlive = false;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && minionTypes.Contains(npc.type))
                {
                    anyAlive = true;
                    break;
                }
            }

            if (!anyAlive)
            {
                BeginPhaseTwo();
            }
        }

        private void SpawnPhaseOneMinions()
        {
            int[] minionTypes =
            {
                ModContent.NPCType<AuricBird>(),
                ModContent.NPCType<AuricSpider>(),
                ModContent.NPCType<AuricHorse>(),
                ModContent.NPCType<AuricSwine>(),
                ModContent.NPCType<AuricWorm>()
            };

            float radius = 400f;

            for (int i = 0; i < minionTypes.Length; i++)
            {
                float angle = MathHelper.TwoPi / minionTypes.Length * i;
                Vector2 offset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;
                Vector2 spawnPos = NPC.Center + offset;

                int newNPC = NPC.NewNPC(
                    NPC.GetSource_FromAI(),
                    (int)spawnPos.X,
                    (int)spawnPos.Y,
                    minionTypes[i],
                    ai0: NPC.whoAmI
                );

                if (Main.netMode == NetModeID.Server && newNPC >= 0)
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, newNPC);

                for (int j = 0; j <20; j++)
                    Dust.NewDust(spawnPos, 10, 10, DustID.GoldFlame);   
            }

            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
        }

        private void BeginPhaseTwo()
        {
            if (transformed)
                return;

            transformed = true;
            phase = 2;

            for (int i = 0; i < 60; i++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldFlame,
                    Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));

            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);

            NPC.dontTakeDamage = false;

            NPC.Transform(ModContent.NPCType<AuricWorldSoulTrue>());            
        }

        private void HandlePhaseTwo()
        {
            //Just transform bro
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<OmegaHealingPotion>();
        }

        public override void OnKill()
        {
            //drops
        }
    }
}



