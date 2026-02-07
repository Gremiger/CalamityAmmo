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
using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Projectiles.Hardmode;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class NapalmBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Napalm Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "汽油弹");
            // Tooltip.SetDefault(IsChinese() ? "一点点火光，谁也烧不到！" : "A little spark can't burn anyone");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Пуля с напалмом");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Маленькая искра никого не может сжечь");
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 2, 85);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<_NapalmBullet>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(70);
            recipe.AddIngredient(ModContent.ItemType< EssenceofHavoc > (), 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 70);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }



    }
}
