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
    public class RottenBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rotten Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "腐烂子弹");
            // Tooltip.SetDefault("Split into several small pieces of rotten matter when fire \n \"It's still crawling and proliferating, and  seems will never be exhausted\"");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "开火时射出一小片霰弹\n“它还在蠕动，增殖，似乎永远也不会被耗尽”");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Гнилая пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "При пожаре распадается на несколько маленьких кусочков гнилой материи \n\"Он все еще ползает и размножается, и, кажется, никогда не истощится\"");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.knockBack = 0.3f;
            Item.value = Item.buyPrice(0, 3, 60, 0);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<_RottenBullet>();
            Item.shootSpeed = 4.8f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<RottenMatter>(), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

    }
}
