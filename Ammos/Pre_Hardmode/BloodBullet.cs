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
    public class BloodBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "血弹");
            // Tooltip.SetDefault("May steal life from the enemies");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "有概率从敌人身上偷取生命");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Кровавая пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Может украсть жизнь у врагов");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 0, 3,0);
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<_BloodBullet>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ModContent.ItemType<BloodOrb>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BloodSample>(), 5);
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
