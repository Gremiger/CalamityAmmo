using CalamityMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace CalamityAmmo.Accessories
{
	public class ArcaneQuiver : ModItem
	{

		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 32;
			Item.value = Item.buyPrice(0, 65, 0, 0);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (!hideVisual)
			{
				modplayer.Arcane2 = true;
			}
			modplayer.Arcane = true;
			player.magicQuiver = true;
		}
		public override void AddRecipes()
		{
			var recipe2 = CreateRecipe();
			recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.MagicQuiver, 1);
			recipe2.AddIngredient(ItemID.ArcaneFlower, 1);
			recipe2.AddIngredient(ItemID.CrystalShard, 5);
			recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
			recipe2.AddTile(TileID.TinkerersWorkbench);
			recipe2.Register();
			recipe2 = CreateRecipe();
			recipe2.AddIngredient(ItemID.StalkersQuiver, 1);
			recipe2.AddIngredient(ItemID.ManaFlower, 1);
			recipe2.ReplaceResult(ModContent.ItemType<ArcaneQuiver>(), 1);
			recipe2.AddIngredient(ItemID.CrystalShard, 5);
			recipe2.AddTile(TileID.TinkerersWorkbench);
			recipe2.Register();
		}
	}
	public class ArcaneArrow_Proj : ModProjectile
	{
		private Vector2[] oldPosi = new Vector2[5];
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 360;
			Projectile.light = 0.5f;
			Projectile.extraUpdates = 0;
			Projectile.arrow = true;
			Projectile.alpha = 255;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return null;
		}
		public override void OnSpawn(IEntitySource source)
		{

		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{

		}
		public override void AI()
		{
			Projectile.ai[0]++;
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
			Projectile.scale = 0.8f;
			CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 300f, Projectile.velocity.Length()+0.325f, 12f);
			if(Projectile.ai[0] <= 10) { if (Projectile.alpha > 0) Projectile.alpha -= 25; }
			if (Projectile.ai[0] >= 150)
			{
				Projectile.velocity *= 0.95f;
				if (Projectile.alpha <= 255) Projectile.alpha += 2;
				else Projectile.Kill();
			}
			if (Projectile.velocity.Length() <= 10f) Projectile.velocity *= 1.1f;
			if (Main.rand.NextBool(2)&&Projectile.velocity.Length()>4.2f)
			{
				Dust num9 = Dust.NewDustPerfect(Projectile.Center,21);
				//num9 *= 0.3f;
				//num9.position.X = Projectile.position.X + Projectile.width - 4f + Main.rand.Next(-4, 5);
				//num9.position.Y = Projectile.position.Y + Projectile.height*2 + Main.rand.Next(-4, 5);
				num9.noGravity = true;
				num9.velocity=Projectile.velocity/2;
				num9.velocity += Main.rand.NextVector2Circular(2f, 2f);
				if (Projectile.ai[0] >= 120)
					num9.alpha = Projectile.alpha/2;
			}
			
		}
		public override bool PreDraw(ref Color lightColor)
		{
			//CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
			return true;
		}
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);
		}
	}
}


