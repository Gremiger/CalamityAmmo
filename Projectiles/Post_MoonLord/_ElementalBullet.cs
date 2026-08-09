using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Materials;
using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.DamageOverTime;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
    public class _ElementalBullet: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Shredded Celestial Carrot");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Shredded Celestial Carrot");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Измельченная Небесная морковь");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;//Does the NPC get invincibility frames based on projectile ID? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, all eight will land damage without the "fake damage" reduction — this is how vanilla's Night Bullet avoids the anti-fake-damage mechanic.)
            Projectile.localNPCHitCooldown = 15;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile ID
            Projectile.usesIDStaticNPCImmunity = false;//Does the NPC get invincibility frames based on projectile type? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, only one hit will land and the rest will pass through — vanilla uses this to cap the damage output of minions.)
            Projectile.idStaticNPCHitCooldown = 10;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile type
            Projectile.netImportant = false;
            Projectile.extraUpdates = 2;
        }
        public override bool? CanCutTiles() => true;
        public override void AI()
        {
            //Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.StardustCellMinionShot, Projectile.damage,Projectile.);
            Projectile.ai[0]++;
            Lighting.AddLight(base.Projectile.Center, Color.White.ToVector3());
            if (Utils.NextBool(Main.rand, 5))
            {
                int num250 = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 66, (float)(base.Projectile.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
                Main.dust[num250].velocity *= 0.2f;
                Main.dust[num250].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
            target.GetGlobalNPC<CAENPC>().elementaldouble = 16f;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			modifiers.ModifyHitInfo += (ref NPC.HitInfo hitInfo) => {

				hitInfo.Damage += (int)(hitInfo.Damage* (target.GetGlobalNPC<CAENPC>().elementaldouble / target.GetGlobalNPC<CAENPC>().elementaldoublemax));
			};
			//Main.NewText(target.GetGlobalNPC<CAENPC>().elementaldouble / target.GetGlobalNPC<CAENPC>().elementaldoublemax);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
            return base.PreDraw(ref lightColor);
        }

        public override bool PreKill(int timeLeft)
        {

            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            return base.PreKill(timeLeft);
        }

    }
}
