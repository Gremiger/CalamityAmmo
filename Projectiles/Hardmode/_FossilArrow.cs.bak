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

    public class _FossilArrow : ModProjectile
    {
        // Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fossil Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        // Token: 0x06002DBB RID: 11707 RVA: 0x00178D78 File Offset: 0x00176F78
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
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
                int num469 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, randomDust, 0f, 0f, 100, default(Color), 1f);
                if (randomDust == 89)
                {
                    Main.dust[num469].scale *= 0.35f;
                }
                Main.dust[num469].velocity *= 0f;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i <= 3; i++)
            {
                Dust.NewDust(Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 78, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 0.8f);
            }
            if (Projectile.owner == Main.myPlayer)
            {
                for (int s = 0; s < Main.rand.Next(2, 5); s++)
                {
                    Vector2 velocity=new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    while (velocity.X == 0f && velocity.Y == 0f)
                    {
                        velocity = new(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                    }
                    velocity.Normalize();
                    velocity *= Main.rand.Next(70, 101) * 0.175f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, ModContent.ProjectileType<_FossilShard2>(), (int)(Projectile.damage * 0.25f), Projectile.knockBack * 0.5f, Projectile.owner, Main.rand.Next(0, 4), 0f);
                }
            }
        }

    }
}





