using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalamityAmmo.Projectiles.Hardmode
{

    public class _NapalmBullet : ModProjectile
    {
        // Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Napalm Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        // Token: 0x06002DBB RID: 11707 RVA: 0x00178D78 File Offset: 0x00176F78
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0.25f;
            Projectile.extraUpdates = 2;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            return false;
        }

        public override void AI()
        {
            if (Utils.NextBool(Main.rand, 5))
            {
                Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 6, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool wasOiled = target.HasBuff(BuffID.Oiled);
            target.AddBuff(BuffID.Oiled, 180);
            if (wasOiled)
            {
                target.AddBuff(BuffID.OnFire3, 120);
                target.AddBuff(ModContent.BuffType<WeakBrimstoneFlames>(), 120);
            }
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound( SoundID.Item14, new Vector2?(Projectile.position));
            for (int i = 0; i < 5; i++)
            {
                int fire = Dust.NewDust(Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[fire].velocity *= 3f;
                if (Utils.NextBool(Main.rand, 2))
                {
                    Main.dust[fire].scale = 0.5f;
                    Main.dust[fire].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 10; j++)
            {
                int fire2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                Main.dust[fire2].noGravity = true;
                Main.dust[fire2].velocity *= 5f;
                fire2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                Main.dust[fire2].velocity *= 2f;
            }
           
            
        }
    }
}



