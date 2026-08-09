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
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using CalamityMod.Buffs.StatDebuffs;

namespace CalamityAmmo.Accessories
{
    public class Explode : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Explosion");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.ArmorPenetration = 30;
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 24;
            Projectile.tileCollide = false ;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;//Does the NPC get immunity frames tracked by projectile ID? (If set to true, if a player fires 8 of this projectile and they all hit an enemy at once, all eight will connect - no fake damage; this is how the vanilla Nightglow's anti-fake-damage works)
            Projectile.localNPCHitCooldown = 60;//Used when the above is set to true; determines how many immunity frames the NPC gets, tracked by projectile ID
            Projectile.usesIDStaticNPCImmunity = false;//Does the NPC get immunity frames tracked by projectile type? (If set to true, if a player fires 8 of this projectile and they all hit an enemy at once, only one will connect and the rest will pass through; vanilla uses this to cap minion damage output)
            Projectile.idStaticNPCHitCooldown = 60;//Used when the above is set to true; determines how many immunity frames the NPC gets, tracked by projectile type
            Projectile.netImportant = false;
        }
        public override bool? CanCutTiles() => true;
        public override void AI()
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame > 8)
            {
                Projectile.frame = 0;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            return base.PreDraw(ref lightColor);
        }
        public override bool PreKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            if (Projectile.Hitbox.Intersects(player.Hitbox))
            {
                player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, Math.Sign(player.Center.X - Projectile.Center.X));
            }
            return base.PreKill(timeLeft);
        }

    }
}

