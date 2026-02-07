using System;
using CalamityMod;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Weapons
{
    public class SpectreRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.damage = 147;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 88;
            Item.height = 30;
            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = 5;
            Item.noMelee = true;
            Item.knockBack = 6f;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = new SoundStyle?(SoundID.Item40);
            Item.autoReuse = false;
            Item.shoot = ProjectileID.LostSoulFriendly;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
            
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += 25f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2?(new Vector2(-10f, 0f));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == 14)
            {
                int proj = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, 297, damage, knockback, player.whoAmI, 2f, 0f);
                Main.projectile[proj].DamageType = DamageClass.Ranged;
                Main.projectile[proj].extraUpdates += 2;
            }
            else
            {
                Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe(1).AddIngredient(ItemID.SpectreBar, 7).AddIngredient<EssenceofEleum>(3).AddIngredient(ItemID.Ectoplasm,3).AddTile(TileID.MythrilAnvil).Register();
        }
    }
}

