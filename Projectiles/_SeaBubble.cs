using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace CalamityAmmo.Projectiles
{
    public class _SeaBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stream");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "水流");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ручей");
        }
    
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.friendly = true; Projectile.hostile = false;
            Projectile.penetrate = 10;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.hostile = false;
            Projectile.timeLeft = 65;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.extraUpdates = 3;
            Projectile.alpha = 255;
        }
        public override bool? CanCutTiles()
        {
            return true;
        }
        public override void AI()
        {

             Projectile.velocity.Y += 0.4f;
            Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.15f, 0.15f, 0.5f);
            int num21 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 172, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.4f);
            Main.dust[num21].noGravity = true;
            Main.dust[num21].noLight = true;
            Main.dust[num21].scale = 1.2f;
            Main.dust[num21].velocity *= -0.75f;
            num21 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180, Projectile.velocity.X, Projectile.velocity.Y, 100, default(Color), 1.4f);
            Main.dust[num21].noGravity = true;
            Main.dust[num21].noLight = true;
            Main.dust[num21].scale = 1.2f;
            Main.dust[num21].velocity *= -0.75f;
        }
    }
}
