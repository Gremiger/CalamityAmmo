using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalamityAmmo.Projectiles.Hardmode
{
    public class _AstralStar : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Astral Star");
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 32;
            base.Projectile.height = 32;
            base.Projectile.friendly = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.alpha = 100;
            base.Projectile.penetrate = 1;
            base.Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            base.Projectile.DamageType = DamageClass.Ranged;
            base.Projectile.localNPCHitCooldown = 10;
            base.Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
            Lighting.AddLight(base.Projectile.Center, 0.3f, 0.5f, 0.1f);
            if (base.Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 20 + Main.rand.Next(40);
                if (Utils.NextBool(Main.rand, 5))
                {
                    SoundEngine.PlaySound(SoundID.Item9, new Vector2?(base.Projectile.position));
                }
            }
            float scaleAmt = Main.mouseTextColor / 200f - 0.35f;
            scaleAmt *= 0.2f;
            Projectile.scale = scaleAmt + 0.95f;
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] > 15f)
            {
                int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[astral].noGravity = true;
                Main.dust[astral].velocity *= 0f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Projectile.type], lightColor, 1, null, true);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color?(new Color(200, 200, 200, base.Projectile.alpha));
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180, false);
        }

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180, true, false);
		}

		public override void Kill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                SoundEngine.PlaySound(SoundID.Item9, new Vector2?(base.Projectile.position));
                Vector2 projSpawn = new(Projectile.Center.X + Main.rand.Next(144) - Main.rand.Next(144), Projectile.Center.Y - 600);
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), projSpawn, Vector2.Normalize(Projectile.Center - projSpawn) * 18,
                                               ModContent.ProjectileType<_AstralStar2>(), Projectile.damage, 2, 0);
                Main.projectile[proj].netUpdate = true;
                Projectile.position = Projectile.Center;
                Projectile.width = (Projectile.height = 96);
                Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
                for (int d = 0; d < 2; d++)
                {
                    Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 50, default(Color), 1f);
                }
                for (int d2 = 0; d2 < 20; d2++)
                {
                    int astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, Utils.Next<int>(Main.rand, this.dustTypes), 0f, 0f, 0, default(Color), 1.5f);
                    Main.dust[astral].noGravity = true;
                    Main.dust[astral].velocity *= 3f;
                    astral = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 50, default(Color), 1f);
                    Main.dust[astral].velocity *= 2f;
                    Main.dust[astral].noGravity = true;
                }
                if (Main.netMode != 2)
                {
                    for (int g = 0; g < 3; g++)
                    {
                        Gore.NewGore(base.Projectile.GetSource_Death(null), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
                    }
                }
            }

        }
        public int[] dustTypes = new int[]
        {
            ModContent.DustType<AstralBlue>(),
            ModContent.DustType<AstralOrange>()
        };
        
    }
}

