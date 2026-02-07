using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Boss;
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
using CalamityAmmo.Projectiles.Hardmode;

namespace CalamityAmmo.Projectiles.Hardmode
{

    public class _AstralMine : ModProjectile
    {
        // Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astral Mine");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
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
            Projectile.timeLeft = 900;
            Projectile.light = 0.5f;
            Projectile.alpha = 144;
            Projectile.usesIDStaticNPCImmunity = true;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
            Projectile.idStaticNPCHitCooldown = 10;
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
            

            base.OnSpawn(source);
        }
        public override void  AI()
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
                int astral = Dust.NewDust(Projectile.position+new Vector2(-20,-20), 10, 10, randomDust, 0f, 0f, 0, default, 1f);
                Main.dust[astral].alpha = Projectile.alpha;
                Main.dust[astral].velocity *= 0f;
                Main.dust[astral].noGravity = true;
            }
            Projectile.ai[0]++;
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 12;
                Projectile.rotation += 0.5f;
            }
            else if (Projectile.alpha <= 0)
            {
                Projectile.velocity *= 0.95f;
            }
            else if (Projectile.alpha <= 0&&Projectile.timeLeft==120)
            {
                Projectile.alpha -= 20;
            }

        }
        public override void Kill(int timeLeft)
        {
                SoundEngine.PlaySound(SoundID.Item14, new Vector2?(base.Projectile.position));
                base.Projectile.position = base.Projectile.Center;
                base.Projectile.width = (base.Projectile.height = 96);
                base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
                base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
                for (int num621 = 0; num621 < 5; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Utils.NextBool(Main.rand, 2))
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 10; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.7f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 1.5f;
                    Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y), base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1f);
                }
                if (Main.myPlayer == base.Projectile.owner)
                {
                    int totalProjectiles = 4;
                    float radians = 6.28318548f / totalProjectiles;
                    float velocity = 1f;
                    double angleA = (double)radians * 0.5;
                    double angleB = (double)MathHelper.ToRadians(90f) - angleA;
                    float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
                    Vector2 spinningPoint = new(-velocityX2, -velocity);
                    for (int i = 0; i < totalProjectiles; i++)
                    {
                        Vector2 velocity2 = Utils.RotatedBy(spinningPoint, (double)(radians * i), default);
                        Projectile.NewProjectile(base.Projectile.GetSource_FromThis(null), Projectile.Center, velocity2*2, ModContent.ProjectileType<_AstralShot>(), (int)(Projectile.damage * 0.2), 0f, Main.myPlayer, 1f, 0f);
                    }
                
            }
        }
        

    }
}



