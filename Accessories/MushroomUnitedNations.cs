using CalamityAmmo.CAESystem;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Accessories
{
	public class MushroomUnitedNations : ModItem
	{
		private const int V = 9;

		public override void SetStaticDefaults()
		{

			Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(4, 8, false));
			ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 32;
			Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
			Item.rare = ItemRarityID.Cyan;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			//modplayer.Evil = true;
			modplayer.Spore = true;
			modplayer.Mycelium = true;
			modplayer.MUN = true;
			//modplayer.Live = true;
			modplayer.Odd2 = true;
		}
		public override void ModifyTooltips(List<TooltipLine> list) => list.IntegrateHotkey(CAEKeybinds.OddHotKey);
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.ShroomiteHeadgear, 1);
			recipe.AddIngredient(ItemID.ShroomiteMask, 1);
			recipe.AddIngredient(ItemID.ShroomiteHelmet, 1);
			recipe.AddIngredient(ItemID.ShroomiteBar, 10);
			//recipe.AddIngredient(ModContent.ItemType<LifeMushroom>(), 1);
			//recipe.AddIngredient(ModContent.ItemType<EvilMushroom>(), 1);
			//recipe.AddIngredient(ModContent.ItemType<FungalCarapace>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MarvelousMycelium>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfectedCrabGill>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OdddMushroom>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Shroomer>(), 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}
	}
	public class Shroomere : ModProjectile
	{
		public Player Owner
		{
			get
			{
				return Main.player[Projectile.owner];
			}
		}
		public override void AI()
		{
			CaePlayer modplayer = Owner.GetModPlayer<CaePlayer>();
			if (modplayer.MUN)
			{
				Projectile.timeLeft = 2;
			}
			if (!modplayer.MUN)
			{
				Projectile.penetrate = 0;
			}
			Projectile.ai[0] += 1;
			Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0.8f, 0.8f, 0.8f);
			Projectile.Center = Owner.Center + new Vector2(0, -100);

			if (Projectile.ai[0] >= 28)
			{
				Vector2 targetPos = Main.player[Projectile.owner].Center;
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];
					if (npc.CanBeChasedBy() && (Vector2.Distance(npc.Center, Projectile.Center) < 1080f))
					{
						if ((targetPos == Main.player[Projectile.owner].Center) ||
							(Vector2.Distance(targetPos, Main.player[Projectile.owner].Center)
							<
							Vector2.Distance(npc.Center, Main.player[Projectile.owner].Center)) &&
				Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
						{
							targetPos = npc.Center;
							Projectile.rotation = Vector2.Normalize(targetPos - Projectile.Center).ToRotation();
							Projectile.direction = Projectile.spriteDirection = (Projectile.Center.X > npc.Center.X) ? -1 : 1;
							//if (Projectile.spriteDirection == -1)
							//{
							//    Projectile.rotation += MathHelper.Pi;
							//}
						}
					}
				}
				if (targetPos != Main.player[Projectile.owner].Center && Main.myPlayer == Projectile.owner)
				{
					SoundEngine.PlaySound(SoundID.Item40, Projectile.position);
					int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(),
					   Projectile.Center + ((Projectile.direction == 1) ? Vector2.Normalize(targetPos - Projectile.Center) * 50 : Vector2.Normalize(targetPos - Projectile.Center) * 50), Vector2.Normalize(targetPos - Projectile.Center) * 16, ProjectileID.BulletHighVelocity, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
					Main.projectile[proj].usesIDStaticNPCImmunity = false;
					Main.projectile[proj].usesLocalNPCImmunity = true;
					Main.projectile[proj].localNPCHitCooldown = 15;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),
						Projectile.Center + ((Projectile.direction == 1) ? Vector2.Normalize(targetPos - Projectile.Center) * 24 : Vector2.Normalize(targetPos - Projectile.Center) * 24), Vector2.Normalize(targetPos - Projectile.Center) * 16, ModContent.ProjectileType<Shroom>(), Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
					Main.projectile[proj].usesIDStaticNPCImmunity = false;
					Main.projectile[proj].usesLocalNPCImmunity = true;
					Main.projectile[proj].localNPCHitCooldown = 15;
				}
				Projectile.ai[0] = 1;
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = (Texture2D)ModContent.Request<Texture2D>(Texture);
			Color drawColor = Projectile.GetAlpha(lightColor);
			Main.EntitySpriteDraw(texture, new Vector2(Projectile.Center.X, Projectile.Center.Y)
 - Main.screenPosition, new Rectangle(0, 0, 90, 28), lightColor, Projectile.rotation, new Vector2(45, 14), Projectile.scale, Projectile.spriteDirection == -1 ? SpriteEffects.FlipVertically : SpriteEffects.None, 0);
			return false;
		}
	}
}

