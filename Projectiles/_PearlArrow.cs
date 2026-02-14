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
using CalamityMod.Items.Accessories;

namespace CalamityAmmo.Projectiles
{
    public class _PearlArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Pearl Arrow");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "珍珠箭");
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Жемчужная стрела");
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
            Projectile.netImportant = true;
            AIType = ProjectileID.WoodenArrowFriendly;
            Projectile.arrow = true;
            Projectile.aiStyle = 1;
        }
        public override bool? CanCutTiles() => true;
        public override void AI()
        {
			// 计算箭尾位置（沿着速度反方向偏移）
			Vector2 tailPosition;

			if (Projectile.velocity != Vector2.Zero)
			{
				// 获取箭的速度方向
				Vector2 direction = Vector2.Normalize(Projectile.velocity);

				// 计算尾部偏移量（箭的长度一半，或者自定义值）
				// 可以根据实际箭的大小调整这个值
				float tailOffset = 10f;

				// 计算尾部位置（从中心向速度反方向偏移）
				tailPosition = Projectile.Center - direction * tailOffset;
			}
			else
			{
				// 如果没有速度，使用当前位置
				tailPosition = Projectile.position;
			}

			// 在尾部生成粒子
			/*for (int i = 0; i < 4; i++)
			{
				int num = Dust.NewDust(tailPosition, base.Projectile.width, base.Projectile.height, DustID.BlueFlare, 0f, 0f, 100, default(Color), 0.6f);
				Main.dust[num].noGravity = true;
				Main.dust[num].velocity *= 0.5f;
				Main.dust[num].velocity += base.Projectile.velocity * 0.1f;
			}*/
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
			if (Projectile.owner == Main.myPlayer)
			{
				// 获取原始弹幕速度的方向
				Vector2 originalDirection = Projectile.velocity;

				originalDirection.Normalize();

				// 计算反方向（向外飞的方向）
				Vector2 outwardDirection = -originalDirection;

				// 随机选择是顺时针还是逆时针旋转
				bool clockwise = Main.rand.NextBool();

				// 随机角度范围：30度到50度
				float randomAngle = MathHelper.ToRadians(Main.rand.NextFloat(30f, 45f));

				// 根据方向应用旋转
				if (clockwise)
				{
					randomAngle = -randomAngle; // 逆时针
				}

				// 应用旋转
				Vector2 finalDirection = outwardDirection.RotatedBy(randomAngle);
				finalDirection.Normalize();

				// 设置向外飞行的速度
				float outwardSpeed = 8f;
				Vector2 outwardVelocity = finalDirection * outwardSpeed;

				// 在爆炸点位置生成弹幕
				Vector2 spawnPosition = Projectile.position-originalDirection*16f;


				// 生成弹幕
				Projectile.NewProjectile(
					Projectile.GetSource_FromThis(),
					spawnPosition.X,
					spawnPosition.Y,
					outwardVelocity.X,
					outwardVelocity.Y,
					ModContent.ProjectileType<PearlAuraShard>(),
					(int)(Projectile.damage * 0.4),
					0f,
					Projectile.owner,
					0f,
					0f
				);
			}
		}
	}
}

