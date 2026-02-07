using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Materials;
using CalamityAmmo.Projectiles;
using CalamityMod.Items.Placeables;
using Terraria.Audio;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityAmmo.Ammos.Post_MoonLord;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Items.Potions.Alcohol;
using CalamityAmmo.CAESystem;

namespace CalamityAmmo.Accessories
{
    public class OdddMushroom : ModItem
    {
        //public static string hotkey = CAEKeybind.OddHotKey.GetAssignedKeys().FirstOrDefault();
		//public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(hotkey);
		public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Odd Mushroom(equipable)");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "致幻蘑菇（可装备）");
			/* Tooltip.SetDefault("When hide visual grains the Trippy buff\n" +
                " You can't always trust what you see."); 
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "隐藏饰品可见性时给予幻菇中毒buff\n" +
                "“眼睛所见的，未必真实”");*/
		}

		public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Odd2 = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OddMushroom>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
		public override void ModifyTooltips(List<TooltipLine> list) => list.IntegrateHotkey(CAEKeybinds.OddHotKey);
	}
}

