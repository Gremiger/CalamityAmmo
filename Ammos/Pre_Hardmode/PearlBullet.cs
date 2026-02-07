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
    public class PearlBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pearl Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "珍珠子弹");
            // Tooltip.SetDefault("Can penetrate two enemies\nWhen hitting the enemies, slow them down");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "可以穿透两个敌人\n命中的敌人会被减速");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Жемчужная пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Может проникнуть сквозь двух врагов\nПри ударе по врагам замедляет их");
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
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 0, 0, 18);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<_PearlBullet>();
            Item.shootSpeed = 12f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ModContent.ItemType<PearlShard>(), 1);
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
