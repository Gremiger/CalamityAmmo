using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class rottenBulletProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中，不骗伤，原版夜明弹的反骗伤就是如此）
			Projectile.localNPCHitCooldown = 15;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
			Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
			Projectile.idStaticNPCHitCooldown = 10;//上一个设定为true则被调用，NPC按照弹幕类型来获取多少无敌帧
			Projectile.netImportant = false;
		}
		public override bool? CanCutTiles() => true;
		public override void AI()
		{
			//Projectile.ai[0]++;

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 180);
			if (target.life > 0 || !target.IsAnEnemy(false, true, true))
			{
				return;
			}
			OnHitEffects(Main.player[Projectile.owner], target, 0);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{

		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{

		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
			return true;
		}

		public override bool PreKill(int timeLeft)
		{

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

			return true;
		}
		private void OnHitEffects(Player player, Entity target, float kb)
		{
			//SoundEngine.PlaySound(ref SoundID.Item74, new Vector2?(target.Center), null);
			for (int i = 0; i < 15; i++)
			{
				int idx = Dust.NewDust(target.position, target.width, target.height, DustID.CorruptPlants, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx].velocity *= 3f;
				if (Utils.NextBool(Main.rand))
				{
					Main.dust[idx].scale = 0.5f;
					Main.dust[idx].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 25; j++)
			{
				int idx2 = Dust.NewDust(target.position, target.width, target.height, 18, 0f, 0f, 100, default(Color), 3f);
				Main.dust[idx2].noGravity = true;
				Main.dust[idx2].velocity *= 5f;
				idx2 = Dust.NewDust(target.position, target.width, target.height, 24, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx2].velocity *= 2f;
			}
			bool useDiagonal = Main.rand.NextBool(); // true=斜角方向，false=正交方向

			Vector2[] directions;

			if (!useDiagonal) // 50%概率：正交方向
			{
				directions = new Vector2[]
				{
			new Vector2(0, -1),   // 上
            new Vector2(0, 1),    // 下
            new Vector2(-1, 0),   // 左
            new Vector2(1, 0)     // 右
				};
			}
			else // 50%概率：斜角方向
			{
				// 标准化对角线方向
				float diagonalNormal = 0.7071f; // 1/√2 ≈ 0.7071
				directions = new Vector2[]
				{
			new Vector2(diagonalNormal, -diagonalNormal),  // 东北
            new Vector2(diagonalNormal, diagonalNormal),   // 东南
            new Vector2(-diagonalNormal, -diagonalNormal), // 西北
            new Vector2(-diagonalNormal, diagonalNormal)   // 西南
				};
			}
			// 生成4个弹幕，分别指向四个方向
			for (int i = 0; i < 4; i++)
			{
				Projectile.NewProjectile(
					Projectile.GetSource_FromThis(),
					target.Center,
					directions[i] * Projectile.velocity.Length(), // 使用预定义的方向
					ModContent.ProjectileType<rottenBulletProj>(),
					Projectile.damage/2,
					kb,
					player.whoAmI,
					Projectile.knockBack
				);
			}
		}

	}
}


