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
    public class AuricWorldSoulTrue : ModNPC
    {
        private int attackTimer;
        private int phaseTimer;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.height = 5000;
            NPC.width = 3000;
            NPC.lifeMax = 5000000;
            NPC.damage = 0;
            NPC.defense = 1524;
            NPC.Calamity().DR = 0.6f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;

            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/AuricWorldSoul");
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead)
                {
                    NPC.velocity.Y -= 0.2f;
                    if (NPC.timeLeft > 10)
                        NPC.timeLeft = 10;
                    return;    
                }
            }

            Vector2 targetPos = player.Center + new Vector2(0, -300);
            Vector2 move = targetPos - NPC.Center;
            float speed = 10f;
            float inertia = 40f;
            NPC.velocity = (NPC.velocity * (inertia - 1) + move.SafeNormalize(Vector2.Zero) * speed) / inertia;

            attackTimer++;
            phaseTimer++;

            if (attackTimer == 120)
                DoProjectileBarrage(player);
            if (attackTimer == 360)
                DoDashAttack(player);
            if (attackTimer > 600) 
                attackTimer = 0;

            Lighting.AddLight(NPC.Center, Color.Gold.ToVector3() * 2f);           
        }

        private void DoProjectileBarrage(Player player)
        {
            SoundEngine.PlaySound(SoundID.Item68, NPC.Center);
            int projectileType = ProjectileID.DeathLaser;
            int damage = 700;
            for (int i = 0; i < 8; i++)
            {
                Vector2 velocity = Vector2.Normalize(player.Center - NPC.Center).RotatedBy(MathHelper.ToRadians(10 * (i - 4))) * 12f;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, velocity, projectileType, damage, 0f, Main.myPlayer);
            }
        }

        private void DoDashAttack(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, NPC.Center);
            Vector2 dashVel = Vector2.Normalize(player.Center - NPC.Center) * 30f;
            NPC.velocity = dashVel;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ModContent.ItemType<OmegaHealingPotion>();
        }

        public override void OnKill()
        {
            SoundEngine.PlaySound(SoundID.Item14, NPC.Center);
            for (int i = 0; i < 60; i++)
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GoldFlame, 
                    Main.rand.NextFloat(-10, 10), Main.rand.NextFloat(-10, 10));

            Main.NewText("Credits: This Boss was made by me", Color.Red);        
        }

        public override void FindFrame(int frameHeight)
        {
            //for later animations
        }
    }
}
