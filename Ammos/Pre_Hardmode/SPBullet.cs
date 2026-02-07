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
using CalamityMod.Items.Placeables;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class SPBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sea Prism Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "棱晶子弹");
            // Tooltip.SetDefault("Release a stream of water during crushing");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "破碎时释放一束洋流");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Морская призматическая пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Выпустить поток воды во время дробления");
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
            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<_PrismBullet>();
            Item.shootSpeed = 12f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ModContent.ItemType<SeaPrism>(), 1);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.WulfrumBoltRanged>(), damage, knockback, Main.myPlayer);
            return true;
        }

    }
}
