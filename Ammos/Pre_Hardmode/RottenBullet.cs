using CalamityAmmo.Projectiles;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class RottenBullet : ModItem
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
            Item.consumable = false;
            Item.knockBack = 0.3f;
            Item.value = Item.buyPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.Orange;
            Item.shoot = ModContent.ProjectileType<rottenBulletProj>();
            Item.shootSpeed = 4.8f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            /*Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.RottenChunk, 12);
            recipe.AddTile(TileID.Anvils);
            recipe.AddCondition(new Condition(Language.GetTextValue(""), () => DownedBossSystem.downedHiveMind));
            recipe.Register();*/
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }

    }
}
