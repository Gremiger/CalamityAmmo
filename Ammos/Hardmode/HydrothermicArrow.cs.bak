using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class HydrothermicArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydrothermic Arrow");
            // Tooltip.SetDefault("You dare strike my shield with your spear? That's gonna end with you in pieces!");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "渊泉箭");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "以子之矛攻子之盾，就会产生大爆炸！");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 19;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 4, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<HydrothermicArrow_Proj>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(150);
            recipe.AddIngredient(ItemID.ExplosivePowder,3);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 6);
            recipe.AddIngredient(ModContent.ItemType<ScoriaBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CoreofHavoc>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
    public class HydrothermicArrow_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydrothermic Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<HydrothermicExplosion>(), (int)(Projectile.damage*1.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
            }
        }
        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }
        public override bool PreAI()
        {
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            if (Utils.NextBool(Main.rand, 4))
            {
                int num469 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Utils.NextBool(Main.rand, 3) ? 16 : 174, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 0f;
            }

            return true;
        }
        public override void Kill(int timeLeft)
        {
            
        }
        public class HydrothermicExplosion : ForbiddenSunburst
        {
            public override string Texture => "CalamityMod/Projectiles/InvisibleProj";
            public override void SetStaticDefaults()
            {

            }
            public override void SetDefaults()
            {
                Projectile.DamageType = DamageClass.Ranged;
                Projectile.ArmorPenetration = 35;
                base.SetDefaults();
            }
           
                
            
            public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
			{
				target.AddBuff(24, 300, false);
			}
		}
    }
}
