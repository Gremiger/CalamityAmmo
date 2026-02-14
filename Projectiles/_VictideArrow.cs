using CalamityAmmo.Misc;
using CalamityAmmo.Projectiles;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityAmmo.Projectiles
{
    public class _VictideArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        }
		private bool hasLiquidBoost = false;
		private int originalExtraUpdates = 0;
		public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
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
            Projectile.aiStyle = ProjAIStyleID.Arrow;
			originalExtraUpdates = Projectile.extraUpdates;
		}
        public override bool? CanCutTiles() => true;
		
		public override void AI()
        {
			if (Projectile.wet && !hasLiquidBoost)
			{
				// 进入液体：增加extraUpdates
				Projectile.extraUpdates = originalExtraUpdates + 1;
				hasLiquidBoost = true;

				// 可以添加视觉效果
				if (Main.rand.NextBool(3))
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
						DustID.Water, 0f, 0f, 100, default, 1f);
					dust.velocity = Projectile.velocity * 0.5f;
					dust.noGravity = true;
				}
			}
			else if (!Projectile.wet && hasLiquidBoost)
			{
				// 离开液体：恢复原始值
				Projectile.extraUpdates = originalExtraUpdates;
				hasLiquidBoost = false;
			}

		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Main.player[Projectile.owner].AddBuff(ModContent.BuffType<tideOfVictory>(), 120);
		}
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
            return base.PreDraw(ref lightColor);
        }
    
      
    }
}


