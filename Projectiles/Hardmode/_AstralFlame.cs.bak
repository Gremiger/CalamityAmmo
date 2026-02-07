using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged
{
    // Token: 0x02000654 RID: 1620
    public class _AstralFlame : ModProjectile
    {
        public override string Texture
        {
            get
            {
                return "CalamityAmmo/Projectiles/Hardmode/_AstralFlame";
            }
        }

        // Token: 0x06002DB3 RID: 11699 RVA: 0x0017884C File Offset: 0x00176A4C
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Astral Crystal");
            Main.projFrames[base.Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        // Token: 0x06002DB4 RID: 11700 RVA: 0x001788A0 File Offset: 0x00176AA0
        public override void SetDefaults()
        {
            base.Projectile.width = 50;
            base.Projectile.height = 50;
            base.Projectile.friendly = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.tileCollide = false ;
            base.Projectile.DamageType = DamageClass.Ranged;
            base.Projectile.alpha = 255;
            base.Projectile.penetrate = 1;
            base.Projectile.timeLeft = 300;
        }

        // Token: 0x06002DB5 RID: 11701 RVA: 0x00178928 File Offset: 0x00176B28
        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
               Projectile.alpha -= 25;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }
            if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float)Math.Atan2(-(double)Projectile.velocity.Y, -(double)Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)base.Projectile.velocity.X);
            }
            Lighting.AddLight(Projectile.Center, 0.3f, 0.5f, 0.1f);
            Projectile.velocity *= 1.04f;
            
            if (speed == 0f)
            {
                speed = Projectile.velocity.Length();
            }
            CalamityUtils.HomeInOnNPC(Projectile, Projectile.tileCollide, 200f, speed, 12f);
        }

        // Token: 0x06002DB6 RID: 11702 RVA: 0x000A64E9 File Offset: 0x000A46E9
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor, 1, null, true);
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180, false);
        }

        public override void Kill(int timeLeft)
        {
           Projectile.position = Projectile.Center;
            Projectile.width = (Projectile.height = 192);
            Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            SoundEngine.PlaySound(SoundID.Zombie103, new Vector2?(Projectile.Center));
            for (int num193 = 0; num193 < 2; num193++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 50, default, 1f);
            }
            for (int num194 = 0; num194 < 20; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 0, default, 1.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 50, default, 1f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
        }
        private float speed;
    }
}
