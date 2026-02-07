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
    public class VictideBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Victide Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "胜潮子弹");
            // Tooltip.SetDefault("Move faster under water and increase damage to 2x \n 'Take my high-pressure gas cylinder '");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在水下移动更快且伤害提升至2倍\n“拿我的高压气瓶来”");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Победоносная пера");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Двигайтесь быстрее под водой и увеличивайте урон в 2 раза\n'Возьмите мой газовый баллон высокого давления");
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
            Item.value = Item.buyPrice(0, 0, 0, 70);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<_VictideBullet>();
            Item.shootSpeed = 4f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.MusketBall, 50);
            recipe.AddIngredient(ModContent.ItemType<SeaRemains>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}

