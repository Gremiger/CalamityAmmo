using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalamityAmmo.Projectiles.Hardmode
{

    public class _FossilBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fossil Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.extraUpdates = 2;        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 150);
            
        }
        public override void OnSpawn(IEntitySource source)
        {

            base.OnSpawn(source);
        }
        public override void AI()
        {
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            int randomDust = Main.rand.Next(4);
            if (randomDust == 3)
            {
                randomDust = 89;
            }
            else if (randomDust == 2)
            {
                randomDust = 75;
            }
            else
            {
                randomDust = 33;
            }
            for (int num468 = 0; num468 < (Utils.NextBool(Main.rand, 2) ? 1 : 2); num468++)
            {
                int num469 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, randomDust, 0f, 0f, 100, default(Color), 0.5f);
                if (randomDust == 89)
                {
                    Main.dust[num469].scale *= 0.3f;
                }
                Main.dust[num469].velocity *= 0f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i <= 2; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, base.Projectile.width, base.Projectile.height, 78, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.8f);
            }
            if (Projectile.owner == Main.myPlayer)
            {
                for (int s = 0; s < Main.rand.Next(2, 5); s++)
                {
                    Vector2 velocity = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    while (velocity.X == 0f && velocity.Y == 0f)
                    {
                        velocity = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    }
                    velocity.Normalize();
                    velocity *= Main.rand.Next(70, 101) * 0.15f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<_FossilShard2>(), (int)(Projectile.damage * 0.15f), Projectile.knockBack * 0.5f, Projectile.owner, Main.rand.Next(0, 4), 0f);
                }
            }
        }

    }
}





