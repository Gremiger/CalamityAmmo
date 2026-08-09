using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.IO;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Weapons.Magic;
using System.Collections.Generic;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod;
using static CalamityAmmo.CAEUtils;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.Items.TreasureBags;
using Terraria.GameContent.ItemDropRules;
using CalamityAmmo.Accessories;
using Terraria.GameContent.Creative;
using Terraria.Localization;

namespace CalamityAmmo.Misc
{
    public class HardTackChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Auric Coffer");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Auric Coffer");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}"); 
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override bool CanRightClick()
        {
            return true;
        }
        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 24;
            Item.height = 24;
            Item.rare = 15;
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
           itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HardTack>(), 1, 15, 40));
           
           //itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<KnowledgeAuric>(), 1000));
        }

    }
}
