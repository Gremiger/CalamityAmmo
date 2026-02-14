using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

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
			if (Projectile.owner == Main.myPlayer)
			{
				for(int i=0; i<2; i++)
				{
					float vectorX = -Projectile.velocity.X * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8.4f);
					float vectorY=-Projectile.velocity.Y * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8.4f);
					Projectile.NewProjectile(
					Projectile.GetSource_FromThis(),
					Projectile.position.X + vectorX,
					Projectile.position.Y+vectorY,
					vectorX,
					vectorY,
					ModContent.ProjectileType<seaPrismShard>(),
					(int)(Projectile.damage * 0.5f),
					0f,
					Projectile.owner
				);
				}
				
			}

		}
	}
}
