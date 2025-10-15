using CalamityMod.Items;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Summon;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Core.Globals
{
    public class ItemRebalance : GlobalItem
    {
        public static float BalanceChange(Item item)
        {
            //Melee
            if (item.type == ItemType<BladecrestOathsword>()) return 0.8f;
            //Ranger
            if (item.type == ItemType<AstralBow>()) return 0.8f; 
            //Mage
            if (item.type == ItemType<Vesuvius>()) return 0.9f;
            //Summon
            if (item.type == ItemType<CosmicImmaterializer>()) return 0.75f;
            //Rogue
            if (item.type == ItemType<Supernova>()) return 0.6f;
            //Classless
            //Multiclass

            return 1;
        }

        public override void SetDefaults(Item item)
        {
            float balance = BalanceChange(item);
            if (balance != 1)
            {
                item.damage = (int)(item.damage * balance);
            }
        }
    }
}
