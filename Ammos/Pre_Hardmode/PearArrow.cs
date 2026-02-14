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
using CalamityMod.Items.Placeables.SunkenSea;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class PearlArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 0, 2, 25);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<_PearlArrow>();
            Item.shootSpeed = 4.5f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(ModContent.ItemType<PearlShard>(), 1);
            //recipe.AddIngredient(ModContent.ItemType<PrismShard>(), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
