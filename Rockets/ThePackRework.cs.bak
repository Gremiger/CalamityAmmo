using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using System.IO;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityAmmo.Accessories;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.EntitySources;
using CalamityMod;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod.NPCs.Bumblebirb;
using CalamityAmmo.Ammos.Post_MoonLord;
using System;
using Terraria.Localization;
using Terraria.Audio;
using Mono.Cecil;

namespace CalamityAmmo.Rockets
{
    public abstract class ThePackRockets : ModProjectile
    {
        private Vector2[] oldPosi = new Vector2[5]; //示例中记录16个坐标用于绘制，你可以试着修改这个值，并思考这意味着什么。
        private int frametime = 0;
        public override bool PreDraw(ref Color lightColor)
        {
            //CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            Projectile.rotation = Projectile.velocity.ToRotation();
            for (int i = oldPosi.Length - 1; i > 0; i--)
            {
                if (oldPosi[i] != Vector2.Zero)
                {
                    if (Projectile.frame < 48)
                    {
                        Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/ThePackRockets").Value,
                        oldPosi[i] - Main.screenPosition,
                        new Rectangle(0, Projectile.frame * 18, 82, 18),
                        Color.White * 1 * (1 - .2f * i),
                        (oldPosi[i - 1] - oldPosi[i]).ToRotation(),
                        new Vector2(41, 9),
                        1, Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

                        //CalamityUtils.DrawAfterimagesCentered(Projectile, 2, lightColor, 1);
                            /*Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/ThePackRockets").Value,
                        Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 18, 82, 18), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(41, 9), Projectile.scale,
                        Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);*/
                       
                    }
                    else
                    {
                        Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture).Value,
                    Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
                    Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                    }
                }
            }
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
         
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("PR");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "PR");
            Main.projFrames[Projectile.type] = 4;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 1;
            Init();
        }
        public virtual void Init()
        {
        }
        public virtual void Inai()
        {
            //帧图！！
        }
        public override void AI()
        {
            Inai();
            Vector2 targetCenter = Projectile.Center;
            float minTargetDistance = 2500f;
            bool homeIn = false;
            if(Main.time % 2 == 0)    //每两帧记录一次（打一次点）
    {
                for (int i = oldPosi.Length - 1; i > 0; i--) //你应该知道为什么这里要写int i = oldVec.Length - 1
                {
                    oldPosi[i] = oldPosi[i - 1];
                }
                oldPosi[0] = Projectile.Center;
            }
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(Projectile, false) && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
                {
                    float distanceFromTarget = Projectile.Center.ManhattanDistance(Main.npc[i].Center);
                    float Linghuodistance = 150f;
                    if (AIType == ProjectileID.RocketIII || AIType == ProjectileID.RocketIV)
                    {
                        Linghuodistance = 200f;
                    }
                    if (AIType == ProjectileID.MiniNukeGrenadeI || AIType == ProjectileID.MiniNukeGrenadeII)
                    {
                        Linghuodistance = 250f;
                    }
                    int rockettype = 0;
                    if (Projectile.type == ModContent.ProjectileType<ThePack_RocketI>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_RocketI>();
                   else if (Projectile.type == ModContent.ProjectileType<ThePack_RocketII>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_RocketII>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_RocketIII>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_RocketIII>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_RocketIV>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_RocketIV>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_ClusterRocketI>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_ClusterRocketI>();
                    else if (Projectile.type == ModContent.ProjectileType< ThePack_ClusterRocketII>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_ClusterRocketII>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_MiniNukeRocketI>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_MiniNukeRocketI>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_MiniNukeRocketII>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_MiniNukeRocketII>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_WetRocket>())
                    rockettype = ModContent.ProjectileType<ThePackLittle_WetRocket>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_LavaRocket>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_LavaRocket>();
                    else if (Projectile.type == ModContent.ProjectileType<ThePack_HoneyRocket>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_HoneyRocket>();
                    else if (Projectile.type == ModContent.ProjectileType< ThePack_DryRocket>())
                        rockettype = ModContent.ProjectileType<ThePackLittle_DryRocket>();

                    if (distanceFromTarget < Linghuodistance)
                    {
                        if (Projectile.owner == Main.myPlayer)
                        {
                            

                            for (int j = 0; j < 5; j++)
                            {
                                Vector2 velocity = Utils.NextVector2Unit(Main.rand, 0f, 6.28318548f) * Utils.NextFloat(Main.rand, 6f, 12f);
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, rockettype, (int)(Projectile.damage * 0.25), Projectile.knockBack, Projectile.owner, 0f, 0f);
                            }
                        }
                        SoundEngine.PlaySound(SoundID.Item14, new Vector2?(Projectile.position));
                        Projectile.Kill();
                        return;
                    }
                    if (distanceFromTarget < minTargetDistance)
                    {
                        minTargetDistance = distanceFromTarget;
                        targetCenter = Main.npc[i].Center;
                        homeIn = true;
                    }
                }
            }
            if (homeIn)
            {
                Projectile.velocity = (Projectile.velocity * 15f + Projectile.SafeDirectionTo(targetCenter, default(Vector2?)) * 30f) / 16f;
                return;
            }
            /* Projectile.frameCounter++;
             if (Projectile.frameCounter > 3)
             {
                 Projectile.frame++;
                 Projectile.frameCounter = 0;
             }
             if (Projectile.frame > 3)
             {
                 Projectile.frame = 0;
             }*/
            if (Projectile.wet && (AIType == ProjectileID.WetRocket || AIType == ProjectileID.LavaRocket || AIType == ProjectileID.HoneyRocket || AIType == ProjectileID.DryRocket))
            {
                Projectile.penetrate = 0;
                return;
            }
            Projectile.ai[0]++;
            Projectile.alpha = Terraria.Utils.Clamp(255 - (int)Projectile.ai[0] * 25, 0, 255);
            if (Projectile.ai[0] < 6) return;
       
                int dust = Dust.NewDust(Projectile.position-Projectile.velocity*5, 18, 18, DustID.Torch);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;
                int goreIndex = Dust.NewDust(Projectile.position - Projectile.velocity * 5, 18, 18, DustID.Smoke);
                //Main.dust[goreIndex].alpha = 66;
                Main.dust[goreIndex].velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(Math.PI / 10) * -1f;

        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                Projectile.type = AIType;
                return true;
            }
            return false;
        }
        public override bool PreKill(int timeLeft)
        {
            Projectile.type = AIType;
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 2;
        }
        public override void Kill(int timeLeft)
        {
            Projectile.width = 300;
            Projectile.height = 300;
            if (AIType == ProjectileID.RocketIII || AIType == ProjectileID.RocketIV)
            {
                Projectile.width = 400;
                Projectile.height = 400;
            }
            if (AIType == ProjectileID.MiniNukeRocketI || AIType == ProjectileID.MiniNukeRocketII)
            {
                Projectile.width = 500;
                Projectile.height = 500;
            }
            Projectile.position.X = Projectile.position.X - Projectile.width / 2;
            Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
            for (int num621 = 0; num621 < 40; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num622].velocity *= 3f;
                if (Utils.NextBool(Main.rand, 2))
                {
                    Main.dust[num622].scale = 0.5f;
                    Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                }
            }
            for (int num623 = 0; num623 < 60; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 255, 0f, 0f, 0, default(Color), 2f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 5f;
                num624 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 255, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num624].velocity *= 2f;
            }
            Projectile.Damage();
        }
    }
    internal class ThePack_RocketI : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketI;
            Projectile.frame = 0;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 3)
            {
                Projectile.frame = 0;
            }

        }
    }
    internal class ThePack_RocketII : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketII;
            Projectile.frame = 4;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 7)
            {
                Projectile.frame = 4;
            }

        }
    }
    internal class ThePack_RocketIII : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketIII;
            Projectile.frame = 9 - 1;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 11)
            {
                Projectile.frame = 9-1;
            }

        }
    }
    internal class ThePack_RocketIV : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.RocketIV;
            Projectile.frame = 13 - 1;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 16 - 1)
            {
                Projectile.frame = 13 - 1;
            }

        }
    }
    internal class ThePack_ClusterRocketI : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.ClusterRocketI;
            Projectile.frame = 16;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 20 - 1)
            {
                Projectile.frame = 17 - 1;
            }

        }
    }
    internal class ThePack_ClusterRocketII : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.ClusterRocketII;
            Projectile.frame = 21 - 1;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 24 - 1)
            {
                Projectile.frame = 21 - 1;
            }

        }
    }
    internal class ThePack_MiniNukeRocketI : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.MiniNukeRocketI;
            Projectile.frame = 24;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 27)
            {
                Projectile.frame = 24;
            }

        }
    }
    internal class ThePack_MiniNukeRocketII : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.MiniNukeRocketII;
            Projectile.frame = 28;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 31)
            {
                Projectile.frame = 28;
            }

        }
    }
    internal class ThePack_WetRocket : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.WetRocket;
            Projectile.frame = 32;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 35)
            {
                Projectile.frame = 32;
            }

        }
    }
    internal class ThePack_LavaRocket : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.LavaRocket;
            Projectile.frame = 36;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 39)
            {
                Projectile.frame = 36;
            }

        }
    }
    internal class ThePack_HoneyRocket : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.HoneyRocket;
            Projectile.frame = 40;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 43)
            {
                Projectile.frame = 40;
            }

        }
    }
    internal class ThePack_DryRocket : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ProjectileID.DryRocket;
            Projectile.frame = 44;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 47)
            {
                Projectile.frame = 44;
            }

        }
    }
    internal class ThePack_SandRocket : ThePackRockets
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void Init()
        {
            AIType = ModContent.ProjectileType<SandRocket_Proj>();
            Projectile.frame = 48;
        }
        public override void Inai()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 47)
            {
                Projectile.frame = 44;
            }
        }
    }
}
