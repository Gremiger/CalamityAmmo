using CalamityMod.Items.Ammo;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Post_MoonLord
{

	public class DivineArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.damage = 22;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 0, 5, 0);
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<DivineArrow_Proj>();
			Item.shootSpeed = 6f;
			Item.ammo = AmmoID.Arrow;

		}

		public override void AddRecipes()
		{
			CreateRecipe(150)
			.AddIngredient(ModContent.ItemType<ElysianArrow>(), 150)
			.AddIngredient(ModContent.ItemType<DivineGeode>(), 1)
			.AddTile(ModContent.TileType<ProfanedCrucible>())
			.Register();
		}


	}
	public class DivineArrow_Proj : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mirage Arrow");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.extraUpdates = 1;
			Projectile.arrow = true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Dig, new Vector2?(Projectile.position), null);
			return true;
		}

		public override void OnSpawn(IEntitySource source)
		{

		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[Projectile.owner];
			if (player.HeldItem.type == ModContent.ItemType<TheStorm>())
			{
				modifiers.FinalDamage *= 0.33f;
			}
			else modifiers.FinalDamage *= 0.66f;
		}
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			return true;
		}
		public override void Kill(int timeLeft)
		{
			Player player = Main.player[Projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			Projectile.position = Projectile.Center;
			Projectile.width = (Projectile.height = 64);
			Projectile.position.X = Projectile.position.X - Projectile.width / 2;
			Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(Projectile.position), null);
			for (int num621 = 0; num621 < 2; num621++)
			{
				int num622 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num622].velocity *= 3f;
				if (Utils.NextBool(Main.rand, 2))
				{
					Main.dust[num622].scale = 0.5f;
					Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int num623 = 0; num623 < 6; num623++)
			{
				int num624 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 3f);
				Main.dust[num624].noGravity = true;
				Main.dust[num624].velocity *= 5f;
				num624 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
				Main.dust[num624].velocity *= 2f;
			}
			if (Main.netMode != 2)
			{
				Vector2 goreSource = Projectile.Center;
				int goreAmt = 3;
				Vector2 source = new(goreSource.X - 24f, goreSource.Y - 24f);
				for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
				{
					float velocityMult = 0.33f;
					if (goreIndex < goreAmt / 3)
					{
						velocityMult = 0.66f;
					}
					if (goreIndex >= 2 * goreAmt / 3)
					{
						velocityMult = 1f;
					}

					int type = Main.rand.Next(61, 64);
					int smoke = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
					Gore gore = Main.gore[smoke];
					gore.velocity *= velocityMult;
					gore.velocity.X = gore.velocity.X + 1f;
					gore.velocity.Y = gore.velocity.Y + 1f;
					type = Main.rand.Next(61, 64);
					smoke = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
					Gore gore2 = Main.gore[smoke];
					gore2.velocity *= velocityMult;
					gore2.velocity.X = gore2.velocity.X - 1f;
					gore2.velocity.Y = gore2.velocity.Y + 1f;
					type = Main.rand.Next(61, 64);
					smoke = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
					Gore gore3 = Main.gore[smoke];
					gore3.velocity *= velocityMult;
					gore3.velocity.X = gore3.velocity.X + 1f;
					gore3.velocity.Y = gore3.velocity.Y - 1f;
					type = Main.rand.Next(61, 64);
					smoke = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
					Gore gore4 = Main.gore[smoke];
					gore4.velocity *= velocityMult;
					gore4.velocity.X = gore4.velocity.X - 1f;
					gore4.velocity.Y = gore4.velocity.Y - 1f;
				}
			}
			float x = Projectile.position.X + Main.rand.Next(-400, 400);
			float y = Projectile.position.Y - Main.rand.Next(500, 800);
			Vector2 vector = new(x, y);
			float num625 = Projectile.position.X + Projectile.width / 2 - vector.X;
			float num626 = Projectile.position.Y + Projectile.height / 2 - vector.Y;
			num625 += Main.rand.Next(-100, 101);
			float num629 = 24;
			float num627 = (float)Math.Sqrt((double)(num625 * num625 + num626 * num626));
			num627 = num629 / num627;
			num625 *= num627;
			num626 *= num627;
			if (Projectile.owner == Main.myPlayer)
			{
				if (Main.rand.NextBool(3))
				{
					int num628 = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), x, y, num625, num626, ModContent.ProjectileType<SkyFlareFriendly>(), Projectile.damage / 2, 5f, Projectile.owner, 0f, 0f, 0f);
					Main.projectile[num628].ai[1] = Projectile.position.Y;
				}
				else
				{
					if (Main.rand.NextBool())
					{
						int num628 = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), x, y, num625, num626, ModContent.ProjectileType<DivineGeodeMeteor>(), Projectile.damage, 5f, Projectile.owner, 0f, 0f, 0f);
						Main.projectile[num628].ai[1] = Projectile.position.Y;
					}
					else
					{
						int num628 = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), x, y, num625, num626, ModContent.ProjectileType<DivineGeodeMeteor2>(), (int)(Projectile.damage * 0.75f), 5f, Projectile.owner, 0f, 0f, 0f);
						Main.projectile[num628].ai[1] = Projectile.position.Y;
					}
				}
			}
		}
	}
	public class DivineGeodeMeteor : SkyFlareFriendly
	{

	}
	public class DivineGeodeMeteor2 : SkyFlareFriendly
	{

	}
}


