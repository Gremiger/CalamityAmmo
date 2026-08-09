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

namespace CalamityAmmo.Accessories
{
    public class ModifiedCoil : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Modified Coil ");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Modified Coil");
            /* Tooltip.SetDefault("Increased ranged damage to 1.07x\nSlightly increases all ranged projectile velocity\n" +
                " May occur electric leakage");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Ranged damage +7%\n" +
                "Slightly increases the flight speed of ranged projectiles\n" +
                "May cause electric leakage");*/
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
            Item.rare = 4;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Coil2 = true;
            player.GetDamage<RangedDamageClass>() *= 1.07f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<WulfrumCoil>());
            recipe.AddIngredient(ItemID.Wire,30);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}

