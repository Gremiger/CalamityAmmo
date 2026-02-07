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
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Typeless;

namespace CalamityAmmo.Projectiles
{
    public class _VictideArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Victide Arrow");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "胜潮箭");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Победоносная стрела");
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
            Projectile.ignoreWater = false;
            Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中）
            Projectile.localNPCHitCooldown = 20;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
            Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透）
            Projectile.idStaticNPCHitCooldown = 15;
            Projectile.netImportant = true;
            Projectile.arrow = true;
            Projectile.aiStyle = 1;
        }
        public override bool? CanCutTiles() => true;
        public override void AI()
        {
            if (Projectile.wet)
            {
                Projectile.extraUpdates = 2;
            }
                
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
       
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.wet)
            {
                modifiers.FinalDamage.Base += Projectile.damage * 0.4f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            return base.PreDraw(ref lightColor);
        }
    
      
    }
}


