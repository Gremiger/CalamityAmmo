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
    public class WulfrumBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wulfrum Scrap Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "废钢弹");
            // Tooltip.SetDefault("Radiate out wulfrum energy beam with slight tracking ability. The beam has 10 base armor penetration  \n Last hamon , son.");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "激发出具有轻微追踪能力的钨钢光线，光线具有10点护甲穿透\n最后的波纹，小子");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Вульфрумская пуля из металлолома");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Излучайте энергетический луч вульфрума с небольшой способностью отслеживания. \nЛуч имеет базовое пробивание брони 10 \n Последний хамон, сынок.");
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
            Item.shoot = ModContent.ProjectileType<WulfrumBoltRanged>();
            Item.shootSpeed = 16f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ModContent.ItemType<EnergyCore>(), 1);
            recipe.AddIngredient(ModContent.ItemType<WulfrumMetalScrap>(), 5);
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
