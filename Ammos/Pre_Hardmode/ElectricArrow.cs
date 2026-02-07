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
using CalamityMod.Projectiles.Ranged;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class ElectricArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Electric Arrow");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "闪电箭");
            // Tooltip.SetDefault("Chasing the storm ");
            //Tooltip.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "“追逐风暴”");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Электрическая стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "В погоне за бурей");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 0, 0, 35);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<BoltArrow>();
            Item.shootSpeed = 5.5f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(15);
            recipe.AddIngredient(ItemID.WoodenArrow, 15);
            recipe.AddIngredient(ModContent.ItemType<StormlionMandible>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}

