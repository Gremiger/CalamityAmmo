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
using CalamityAmmo.Projectiles;
using System.Drawing.Text;
using System.Runtime.CompilerServices;

namespace CalamityAmmo.Projectiles
{
    public class _PrismBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("PrsimBullet");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "棱晶子弹");
            Main.projFrames[Projectile.type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Морская призматическая пуля");
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
            Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中，不骗伤，原版夜明弹的反骗伤就是如此）
            Projectile.localNPCHitCooldown = 15;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
            Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
            Projectile.idStaticNPCHitCooldown = 10;//上一个设定为true则被调用，NPC按照弹幕类型来获取多少无敌帧
            Projectile.netImportant = false;
            Projectile.extraUpdates = 2;
        }
        public override bool? CanCutTiles() => true;
        public override void AI()
        {
            
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            return base.PreDraw(ref lightColor);
        }
       
        public override bool PreKill(int timeLeft)
        {

                Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            return base.PreKill(timeLeft);
        }
        public override void OnKill(int timeLeft)
        {
            if ( Projectile.owner == Main.myPlayer)
            {
         
                    // Calculate new speeds for other projectiles.
                    // Rebound at 40% to 70% speed, plus a random amount between -8 and 8
                    float speedX = Projectile.velocity.X * Main.rand.NextFloat(.45f, .55f) + Main.rand.NextFloat(-8f, 8f);
                    float speedY = Projectile.velocity.Y * Main.rand.Next(45, 55) * 0.01f + Main.rand.Next(-20, 21) * 0.4f; // This is Vanilla code, a little harder to comprehend. This is just here to teach you that you can convert vanilla code to more readable code sometimes.

                    // Spawn the Projectile.
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position.X + speedX, Projectile.position.Y + speedY, speedX, speedY, ModContent.ProjectileType<_SeaBubble>(), Projectile.damage, 0f, Projectile.owner, 0f, 0f);
          
            }
            
        }
    }
}
