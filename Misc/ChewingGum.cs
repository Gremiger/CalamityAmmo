using System;
using CalamityMod;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc

{
    public class ChewingGum : ModItem
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Chewing Gum");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "专注口香糖");
			/* Tooltip.SetDefault("Increases view range for guns (Right click to zoom out)\n" +
                "When holding a gun, display a line of sight and replace cursor with a sight bead"); 
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "增加枪的可视范围（右键点击可缩小）\n" +
                "手持枪械类武器时，显示一条瞄准线，并将光标替换为准星");*/
		}

		public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 30;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.value = Item.sellPrice(0, 0, 0, 5);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle?(SoundID.Item8);
            Item.autoReuse = false ;
            Item.buffType = ModContent.BuffType<Focus>();
            Item.buffTime = 60 * 60 * 12;
            Item.consumable = true;
            Item.maxStack = 9999;

        }
        public override void AddRecipes()
        {
            CreateRecipe(6).AddIngredient(ItemID.Gel, 18).AddTile(TileID.WorkBenches).Register();
        }
    }
}

