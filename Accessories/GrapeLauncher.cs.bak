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
using CalamityMod.Projectiles.Melee;
using CalamityAmmo.Projectiles.Post_MoonLord;
using Terraria.Audio;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.Dusts;
using CalamityMod.NPCs.NormalNPCs;
using static Terraria.ModLoader.PlayerDrawLayer;
using CalamityMod.Items;
using CalamityAmmo.Ammos.Post_MoonLord;

namespace CalamityAmmo.Accessories
{
    public class GrapeLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Grapeshot");
            ////DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "爆裂葡萄");
            // Tooltip.SetDefault("There is a zombie on your lawn");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "草地上有个脏比");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 34;
            Item.damage = 65;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useAnimation = 40;
            Item.useStyle = 1;
            Item.useTime = 40;
            Item.knockBack = 1f;
            Item.UseSound = new SoundStyle?(SoundID.Item1);
            Item.autoReuse = true;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.value = CalamityGlobalItem.Rarity1BuyPrice;
            Item.rare = 3;
            Item.shoot = ModContent.ProjectileType<GrapeProj>();
            Item.shootSpeed = 20f;
            Item.DamageType = DamageClass.Ranged;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BouncyGrenade, 1);
            recipe.AddIngredient(ItemID.Grapes, 1);
            recipe.AddTile(TileID.HeavyWorkBench);
            recipe.Register();
            var recipe2 = CreateRecipe();
            recipe2 = CreateRecipe();
            recipe2.AddIngredient(ItemID.Grapes, 1);
            recipe2.AddIngredient(ItemID.Grenade, 1);
            recipe2.ReplaceResult(ItemID.PinkGel, 1);
            recipe2.AddTile(TileID.HeavyWorkBench);
            recipe2.Register();
        }
    }
    public class GrapeProj:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("GrapeExplosion");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.width = 22;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0]>7)
            Projectile.velocity.Y += 1f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.Kill();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.Kill();
            return true;
        }

        public override void Kill(int timeLeft)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                Player player = Main.player[Projectile.owner];
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, new Vector2(0, 0), ModContent.ProjectileType<Explode>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                for (int i = 0; i <= 7; i++)
                    {
                        Vector2 finalVec = ( i * MathHelper.Pi / 4.0f).ToRotationVector2() * 16;
                        int grape=Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, finalVec, ModContent.ProjectileType<GrapeShot>(), (int)(Projectile.damage * 0.3f), 0f, Projectile.owner);
                    Main.projectile[grape].tileCollide = true;
                }
            }
        }
    }
    public class GrapeShot:ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ArmorPenetration = 30;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = ProjectileID.BallofFrost;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 1000;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {
                Projectile.Kill();
            }
            else
            {
                if (Projectile.velocity.X != oldVelocity.X)
                {
                    Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                    Projectile.velocity.Y = -oldVelocity.Y;
                }
                SoundEngine.PlaySound( SoundID.Item10, new Vector2?(Projectile.position));
            }
            return false;
        }
    }
}


