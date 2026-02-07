using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
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

    public class _AstralBullet : ModProjectile
    {
        // Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Bullet");
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
            
            AIType = 14;
            Projectile.Calamity().pointBlankShotDuration = 18;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
          int numberProjectiles = 1 + Main.rand.Next(3);
             if (Main.rand.NextBool(5))
             {
                 Player player = Main.player[Projectile.owner];
                 Projectile.damage = (int)(Projectile.damage * 0.5f);
                 for (int i = 0; i < numberProjectiles; i++)
                 {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center + new Vector2(0, 48), Projectile.velocity, ModContent.ProjectileType<_AstralFlame>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                    Main.projectile[proj].tileCollide = false;
                    Main.projectile[proj].netUpdate = true;
                }
                 for (int j = 4 - numberProjectiles; j > 0; j--)
                 {
                    int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center + new Vector2(0, -48), Projectile.velocity, ModContent.ProjectileType<_AstralFlame>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
                    Main.projectile[proj].tileCollide = false;
                    Main.projectile[proj].netUpdate = true;
                }


             }
         
                        base.OnSpawn(source);
                    }
                
            
        
        public override bool PreAI()
        {
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 4f && Utils.NextBool(Main.rand, 2))
            {
                int randomDust = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                    ModContent.DustType<AstralOrange>(),
                    ModContent.DustType<AstralBlue>()
                });
                int astral = Dust.NewDust(Projectile.position, 1, 1, randomDust, 0f, 0f, 0, default(Color), 0.5f);
                Main.dust[astral].alpha = Projectile.alpha;
                Main.dust[astral].velocity *= 0f;
                Main.dust[astral].noGravity = true;
            }
            if (speed == 0f)
            {
                speed = Projectile.velocity.Length();
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, speed, 12f);
            
            return false;
        }


        private float speed;
    }
}



