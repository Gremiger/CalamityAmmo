using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class _RottenBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rotten Bullet");
			//DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "腐烂物");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Гнилая пуля");
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
			Projectile.alpha = 0;
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

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 240);
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
		public override void OnSpawn(IEntitySource source)
		{


		}
	}
}




