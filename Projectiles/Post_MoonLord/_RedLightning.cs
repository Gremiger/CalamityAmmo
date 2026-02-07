using System;
using System.IO;
using CalamityMod;
using CalamityMod.NPCs.Bumblebirb;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
    // Token: 0x02000AA8 RID: 2728
    public class _RedLightning : ModProjectile
    {
        // Token: 0x1700096E RID: 2414
        // (get) Token: 0x06005274 RID: 21108 RVA: 0x002ADFC5 File Offset: 0x002AC1C5
        public override string Texture
        {
            get
            {
                return "CalamityAmmo/Projectiles/Post_MoonLord/LightningProj";
            }
        }

        // Token: 0x06005275 RID: 21109 RVA: 0x002D8772 File Offset: 0x002D6972
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Red Lightning");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "孙红雷");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Красная Молния");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
        }

        // Token: 0x06005276 RID: 21110 RVA: 0x002D87AC File Offset: 0x002D69AC
        public override void SetDefaults()
        {
            Projectile.Calamity().DealsDefenseDamage = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 20;
            Projectile.timeLeft = 2000;
        }

        public override void AI()
        {

            if (Projectile.frameCounter == 0 || Projectile.oldPos[0] == Vector2.Zero)
            {
                for (int num31 = Projectile.oldPos.Length - 1; num31 > 0; num31--)
                {
                    Projectile.oldPos[num31] = Projectile.oldPos[num31 - 1];
                }
                Projectile.oldPos[0] = Projectile.position;
                if (Projectile.velocity == Vector2.Zero)
                {
                    float num32 = (float)(Projectile.rotation + Math.PI / 2 + (Main.rand.NextBool(2) ? -1f : 1f) * Math.PI / 2);
                    float num33 = (float)Main.rand.NextDouble() * 2f + 2f;
                    Vector2 vector2 = new((float)Math.Cos((double)num32) * num33, (float)Math.Sin((double)num32) * num33);
                    int num34 = Dust.NewDust(Projectile.oldPos[Projectile.oldPos.Length - 1], 0, 0, DustID.RedTorch, vector2.X, vector2.Y, 0, default, 1f);
                    Main.dust[num34].noGravity = true;
                    Main.dust[num34].scale = 1.7f;
                }
            }
            int num35 = Projectile.frameCounter;
            Projectile.frameCounter = num35 + 1;
            Lighting.AddLight(Projectile.Center, 0.8f, 0.25f, 0.15f);
            if (Projectile.velocity == Vector2.Zero)
            {
                if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
                {
                    Projectile.frameCounter = 0;
                    bool flag36 = true;
                    for (int num36 = 1; num36 < Projectile.oldPos.Length; num36 = num35 + 1)
                    {
                        if (Projectile.oldPos[num36] != Projectile.oldPos[0])
                        {
                            flag36 = false;
                        }
                        num35 = num36;
                    }
                    if (flag36)
                    {
                        Projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(Projectile.extraUpdates) == 0)
                {
                    for (int num37 = 0; num37 < 2; num37 = num35 + 1)
                    {
                        float num38 = Projectile.rotation + (Main.rand.Next(2) == 1 ? -1f : 1f) * 1.57079637f;
                        float num39 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector3 = new((float)Math.Cos((double)num38) * num39, (float)Math.Sin((double)num38) * num39);
                        int num40 = Dust.NewDust(Projectile.Center, 0, 0, 60, vector3.X, vector3.Y, 0, default, 1f);
                        Main.dust[num40].noGravity = true;
                        Main.dust[num40].scale = 1.2f;
                        num35 = num37;
                    }
                    if (Main.rand.NextBool(5))
                    {
                        Vector2 value39 = Projectile.velocity.RotatedBy(1.5707963705062866, default) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
                        int num41 = Dust.NewDust(Projectile.Center + value39 - Vector2.One * 4f, 8, 8, 60, 0f, 0f, 100, default, 1.5f);
                        Main.dust[num41].velocity *= 0.5f;
                        Main.dust[num41].velocity.Y = -Math.Abs(Main.dust[num41].velocity.Y);
                        return;
                    }
                }
            }
            else if (Projectile.frameCounter >= Projectile.extraUpdates * 2)
            {
                Projectile.frameCounter = 0;
                float num42 = Projectile.velocity.Length();
                UnifiedRandom unifiedRandom = new UnifiedRandom((int)Projectile.ai[1]);
                int num43 = 0;
                Vector2 vector4 = -Vector2.UnitY;
                Vector2 vector5;
                do
                {
                    int num44 = unifiedRandom.Next();
                    Projectile.ai[1] = num44;
                    num44 %= 100;
                    vector5 = (num44 / 100f * 6.28318548f).ToRotationVector2();
                    if (vector5.Y > 0f)
                    {
                        vector5.Y *= -1f;
                    }
                    bool flag37 = false;
                    if (vector5.Y > -0.02f)
                    {
                        flag37 = true;
                    }
                    if (vector5.X * (Projectile.extraUpdates + 1) * 2f * num42 + Projectile.localAI[0] > 40f)
                    {
                        flag37 = true;
                    }
                    if (vector5.X * (Projectile.extraUpdates + 1) * 2f * num42 + Projectile.localAI[0] < -40f)
                    {
                        flag37 = true;
                    }
                    if (!flag37)
                    {
                        goto IL_58F;
                    }
                    num35 = num43;
                    num43 = num35 + 1;
                }
                while (num35 < 100);
                Projectile.velocity = Vector2.Zero;
                Projectile.localAI[1] = 1f;
                goto IL_593;
            IL_58F:
                vector4 = vector5;
            IL_593:
                if (Projectile.velocity != Vector2.Zero)
                {
                    Projectile.localAI[0] += vector4.X * (Projectile.extraUpdates + 1) * 2f * num42;
                    Projectile.velocity = vector4.RotatedBy((double)(Projectile.ai[0] + Math.PI / 6 * 5 + Main.rand.Next(10)), default) * num42;
                    Projectile.rotation = Projectile.velocity.ToRotation() + 1.57079637f;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(144, 120);
            
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextBool(114514) && !NPC.AnyNPCs(ModContent.NPCType<Bumblefuck>()))
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Bumblefuck>());
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<Bumblefuck>(), 0f, 0f, 0, 0, 0);
                }
            base.OnSpawn(source);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 end = Projectile.position + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
            Texture2D tex3 = ModContent.Request<Texture2D>("CalamityAmmo/Projectiles/Post_MoonLord/RedLightningTexture", (ReLogic.Content.AssetRequestMode)2).Value;
            Projectile.GetAlpha(lightColor);
            Vector2 scale16 = new Vector2(Projectile.scale) / 2f;
            for (int num289 = 0; num289 < 3; num289++)
            {
                if (num289 == 0)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(219, 104, 58, 0) * 0.5f;
                }
                else if (num289 == 1)
                {
                    scale16 = new Vector2(Projectile.scale) * 0.4f;
                    DelegateMethods.c_1 = new Color(255, 126, 56, 0) * 0.5f;
                }
                else
                {
                    scale16 = new Vector2(Projectile.scale) * 0.2f;
                    DelegateMethods.c_1 = new Color(255, 128, 128, 0) * 0.5f;
                }
                DelegateMethods.f_1 = 1f;
                for (int num290 = Projectile.oldPos.Length - 1; num290 > 0; num290--)
                {
                    if (!(Projectile.oldPos[num290] == Vector2.Zero))
                    {
                        Vector2 start = Projectile.oldPos[num290] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = Projectile.oldPos[num290 - 1] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (Projectile.oldPos[0] != Vector2.Zero)
                {
                    DelegateMethods.f_1 = 1f;
                    Vector2 start2 = Projectile.oldPos[0] + new Vector2(Projectile.width, Projectile.height) / 2f + Vector2.UnitY * Projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, scale16, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }
    }
}

