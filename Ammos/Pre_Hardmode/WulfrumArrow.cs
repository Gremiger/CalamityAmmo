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

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class WulfrumArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wulfrum Arrow");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "钨钢箭");
            // Tooltip.SetDefault("It's too heavy to make arrows at all. \n 'really ?' ");
            //Tooltip.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "太重，太沉，钨钢根本不适合造箭。\n“真的吗？”");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Вульфрум Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Он слишком тяжелый, чтобы вообще делать стрелы. \n'правда?'");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 12f;
            Item.value = Item.buyPrice(0, 0, 0, 35);
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<_WulfArrow>();
            Item.shootSpeed = 0f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 1);
            recipe.AddIngredient(ModContent.ItemType<WulfrumMetalScrap>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        
    }
}
