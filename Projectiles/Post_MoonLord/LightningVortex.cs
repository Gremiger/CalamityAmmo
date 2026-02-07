using CalamityMod.Buffs.DamageOverTime;
using CalamityMod;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
    public class LightningVortex : ModProjectile
    {
        private bool hasHitEnemy;

        private static int Lifetime = 300;

        private static int ReboundTime = 100;

        private int targetNPC = -1;

        private List<int> previousNPCs = new List<int> { -1 };
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Lightning Vortex");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Вихрь молнии");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "星旋闪电");
            ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 16;
            base.Projectile.height = 50;
            base.Projectile.friendly = true;
            base.Projectile.tileCollide = false;
            base.Projectile.penetrate = 8;
            base.Projectile.timeLeft = Lifetime;
            base.Projectile.DamageType = DamageClass.Ranged;
            base.Projectile.extraUpdates = 3;
            base.Projectile.usesLocalNPCImmunity = true;
            base.Projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(4))
            {
                int vortex = Dust.NewDust(Projectile.position+Projectile.velocity , Projectile.width, Projectile.height, DustID.Vortex, -Projectile.velocity.X*0.8f, -Projectile.velocity.Y * 0.8f);
                Main.dust[vortex].noGravity = true;
                Main.dust[vortex].scale = 0.5f + Main.rand.NextFloat();
                Main.dust[vortex].fadeIn = 0.4f;
                int vortex2= Dust.NewDust(Projectile.position+ Projectile.velocity, Projectile.width, Projectile.height , DustID.Granite, -Projectile.velocity.X * 0.8f, -Projectile.velocity.Y * 0.8f);
                Main.dust[vortex2].noGravity = true;
                Main.dust[vortex2].scale = 0.3f + Main.rand.NextFloat();
                Main.dust[vortex2].fadeIn = 0.5f;
                Main.dust[vortex2].alpha = 150;
            }
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 8;
                SoundEngine.PlaySound(in SoundID.Item7, Projectile.position);
            }

            if (Projectile.timeLeft < Lifetime - ReboundTime)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                return;
            }
            

            if (!hasHitEnemy)
            {
                float distance = 550f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].CanBeChasedBy(Projectile) && Vector2.Distance(Main.npc[i].Center, Projectile.Center) < distance)
                    {
                        Vector2 velocity = Main.npc[i].Center - Projectile.Center;
                        velocity.Normalize();
                        velocity *= 15f;
                        Projectile.velocity = velocity;
                    }
                }
            }
            else if (targetNPC >= 0)
            {
                Vector2 velocity2 = Main.npc[targetNPC].Center - Projectile.Center;
                velocity2.Normalize();
                velocity2 *= 15f;
                Projectile.velocity = velocity2;
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, Projectile.velocity.Length(), 12f); ;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 200);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 180);
            if (Projectile.penetrate != -1)
            {
                float num = 550f;
                int num2 = 0;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    bool flag = false;
                    for (int j = 0; j < previousNPCs.Count; j++)
                    {
                        if (previousNPCs[j] == i)
                        {
                            flag = true;
                        }
                    }
                    NPC nPC = Main.npc[i];
                    if (nPC == target)
                    {
                        previousNPCs.Add(i);
                    }

                    if (nPC.CanBeChasedBy(Projectile) && nPC != target && !flag)
                    {
                        float num3 = (Projectile.Center - nPC.Center).Length();
                        if (num3 < num)
                        {
                            num = num3;
                            num2 = i;
                        }
                    }
                }
                if (num < 550f)
                {
                    hasHitEnemy = true;
                    targetNPC = num2;
                    Vector2 velocity = Main.npc[num2].Center - Projectile.Center;
                    velocity.Normalize();
                    velocity *= 15f;
                    Projectile.velocity = velocity;
                }
                else
                {
                    Projectile.penetrate = 0;
                    Projectile.ai[0] = 1f;
                    targetNPC = -1;
                }
            }

            if (Projectile.penetrate == 1)
            {
                
                Projectile.ai[0] = 1f;
                targetNPC = -1;
                Projectile.penetrate = 0;
            }
        }
    }
}
    
