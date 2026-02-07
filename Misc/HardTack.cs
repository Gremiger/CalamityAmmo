using System;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Potions;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc

{
    public class HardTack : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hard Tack");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "圣金源压缩饼干");
            // Tooltip.SetDefault("{$CommonItemTooltip.MajorStats}\nJust one look and you're full");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "{$CommonItemTooltip.MajorStats}\n光是看上一眼就饱了");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.buyPrice(0, 10, 0, 0);
			//Item.value = Item.sellPrice(0,10,0,0);
			Item.rare = ModContent.RarityType<Violet>();
            Item.UseSound = new SoundStyle?(SoundID.Item8);
            Item.autoReuse = false;
            Item.buffType = BuffID.WellFed3;
            Item.buffTime = 60 * 60 * 18;
            Item.consumable = true;
            Item.maxStack = 9999;
            
        }
        public override bool CanUseItem(Player player)
        {
            if(player.HasBuff(207))
            {
                return false;
            }
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(30);
            recipe.AddIngredient(ModContent.ItemType<AuricBar>(), 1);
            recipe.AddIngredient(ItemID.DirtBlock, 30);
            recipe.AddTile(ModContent.TileType<DraedonsForge>());
            recipe.Register();
        }
    }
}

