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

namespace CalamityAmmo.Rockets
{
    public abstract class ThePackLittleRockets : ModProjectile
    {
        public override bool PreDraw(ref Color lightColor)
        {
            //CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            Projectile.rotation = Projectile.velocity.ToRotation();
   
                    if (Projectile.frame < 48)
                    {
                        /*Main.spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/ThePackLittleRockets").Value,
                        oldPosi[i] - Main.screenPosition,
                        new Rectangle(0, Projectile.frame * 18, 82, 18),
                        Color.White * 1 * (1 - .2f * i),
                        (oldPosi[i - 1] - oldPosi[i]).ToRotation(),
                        new Vector2(41, 9),
                        1, Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);*/

                        //CalamityUtils.DrawAfterimagesCentered(Projectile, 2, lightColor, 1);
                        Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/ThePackRocketsLittle").Value,
                    Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 14, 30, 14), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(15, 7), Projectile.scale,
                    Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);

                    }
                    else
                    {
                        Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture).Value,
                    Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
                    Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
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
            Projectile.width = 30;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true ;
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
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X);
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 300f, 12f, 15f);
            if (Projectile.wet && (AIType == ProjectileID.WetRocket || AIType == ProjectileID.LavaRocket || AIType == ProjectileID.HoneyRocket || AIType == ProjectileID.DryRocket))
            {
                Projectile.penetrate = 0;
                return;
            }

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
            SoundEngine.PlaySound(SoundID.Item14, new Vector2?(Projectile.position));
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 255, 0f, 0f, 0, default(Color), 1f);
            }
        }
    }
    internal class ThePackLittle_RocketI : ThePackLittleRockets
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
    internal class ThePackLittle_RocketII : ThePackLittleRockets
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
    internal class ThePackLittle_RocketIII : ThePackLittleRockets
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
                Projectile.frame = 9 - 1;
            }

        }
    }
    internal class ThePackLittle_RocketIV : ThePackLittleRockets
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
    internal class ThePackLittle_ClusterRocketI : ThePackLittleRockets
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
    internal class ThePackLittle_ClusterRocketII : ThePackLittleRockets
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
    internal class ThePackLittle_MiniNukeRocketI : ThePackLittleRockets
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
    internal class ThePackLittle_MiniNukeRocketII : ThePackLittleRockets
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
    internal class ThePackLittle_WetRocket : ThePackLittleRockets
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
    internal class ThePackLittle_LavaRocket : ThePackLittleRockets
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
    internal class ThePackLittle_HoneyRocket : ThePackLittleRockets
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
    internal class ThePackLittle_DryRocket : ThePackLittleRockets
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
}

