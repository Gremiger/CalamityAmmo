using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Weapons

{
	public class ChewingGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chewing Gun");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "口枪糖");
			// Tooltip.SetDefault("barely-usable");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "勉强能冲");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 88;
			Item.height = 30;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 0f;
			Item.value = Item.sellPrice(0, 0, 0, 20);
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.NPCHit1;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.LostSoulFriendly;
			Item.shootSpeed = 12f;
			Item.useAmmo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe(1).AddIngredient(ItemID.Gel, 18).AddTile(TileID.WorkBenches).Register();
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.Next(100) >= 33;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			int proj = Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<StcikyChewingGum>(), damage + 1, knockback, player.whoAmI, 2f, 0f);
			//Main.projectile[proj].extraUpdates += 1;
			Main.projectile[proj].velocity.Y += 0.35f;
			return false;
		}
	}
	public class StcikyChewingGum : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "CalamityMod/Items/Weapons/Rogue/WebBall";
			}
		}
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chewing Gum");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "口香糖");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.DontAttachHideToAlpha[Type] = true;
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 22;
			Projectile.friendly = true; Projectile.hostile = false;
			Projectile.penetrate = 3;
			Projectile.timeLeft = 600;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.hide = true;
		}
		public override bool? CanCutTiles()
		{
			return false;
		}
		/*
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            int frameheight = texture.Height / Main.projFrames[Projectile.type];
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, frameheight * Projectile.frame, texture.Width, frameheight), lightColor, Projectile.rotation,
            new Vector2(texture.Width / 2, frameheight / 2), Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
		*/
		// See ExampleBehindTilesProjectile. 
		public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{
			// If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
			if (Projectile.ai[0] == 1f) // or if(isStickingToTarget) since we made that helper method.
			{
				int npcIndex = (int)Projectile.ai[1];
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					if (Main.npc[npcIndex].behindTiles)
					{
						behindNPCsAndTiles.Add(index);
					}
					else
					{
						behindNPCs.Add(index);
					}

					return;
				}
			}
			// Since we aren't attached, add to this list
			behindProjectiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// Inflate some target hitboxes if they are beyond 8,8 size
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			// Return if the hitboxes intersects, which means the javelin collides or not
			return projHitbox.Intersects(targetHitbox);
		}

		public override void OnKill(int timeLeft)
		{
			Terraria.Audio.SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position); // Play a death sound
			Vector2 usePos = Projectile.Center; // Position to use for dusts

			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (Projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
																									   //usePos += rotVector * 16f;

			// Declaring a constant in-line is fine as it will be optimized by the compiler
			// It is however recommended to define it outside method scope if used elswhere as well
			// They are useful to make numbers that don't change more descriptive
			const int NUM_DUSTS = 20;

			// Spawn some dusts upon javelin death
			for (int i = 0; i < NUM_DUSTS; i++)
			{
				// Create a new dust
				Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.t_Slime);
				//dust.position = (dust.position + Projectile.Center) / 2f;
				//dust.velocity += rotVector * 2f;
				dust.velocity *= 0.7f;
				dust.noGravity = false;
				//usePos -= rotVector * 8f;
			}

			// Make sure to only spawn items if you are the Projectile owner.
			// This is an important check as Kill() is called on clients, and you only want the item to drop once
			if (Projectile.owner == Main.myPlayer)
			{
				// Drop a javelin item, 1 in 18 chance (~5.5% chance)
				/*int item =
                Main.rand.NextBool(18)
                    ? Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<ItemEvilJavelin>())
                    : 0;*/

				// Sync the drop for multiplayer
				// Note the usage of Terraria.ID.MessageID, please use this!
				/* if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				 {
					 NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				 }*/
			}
		}
		public bool IsStickingToTarget
		{
			get => Projectile.ai[0] == 1f;
			set => Projectile.ai[0] = value ? 1f : 0f;
		}

		public int TargetWhoAmI
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		private const int MAX_STICKY_JAVELINS = 8;
		private readonly Point[] _stickingJavelins = new Point[MAX_STICKY_JAVELINS];

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			IsStickingToTarget = true; // we are sticking to a target
			TargetWhoAmI = target.whoAmI; // Set the target whoAmI
			Projectile.timeLeft = 600;
			Projectile.velocity = (target.Center - Projectile.Center) * 0.75f; // Change velocity based on delta center of targets (difference between entity centers)
			Projectile.netUpdate = true; // netUpdate this javelin
			target.AddBuff(137, 600);
			if (target.noGravity)
				target.position -= target.velocity * 0.8f;
			else
			{
				target.position.X -= target.velocity.X * 0.8f;
				if (target.velocity.Y < 0) target.position.Y -= target.velocity.Y * 0.8f;
			}
			Projectile.damage = 0; // Makes sure the sticking javelins do not deal damage anymore

			// It is recommended to split your code into separate methods to keep code clean and clear
			UpdateStickyJavelins(target);
		}
		private void UpdateStickyJavelins(NPC target)
		{
			int currentJavelinIndex = 0; // The javelin index

			for (int i = 0; i < Main.maxProjectiles; i++) // Loop all Projectiles
			{
				Projectile currentProjectile = Main.projectile[i];
				if (i != Projectile.whoAmI // Make sure the looped Projectile is not the current javelin
					&& currentProjectile.active // Make sure the Projectile is active
					&& currentProjectile.owner == Main.myPlayer // Make sure the Projectile's owner is the client's player
					&& currentProjectile.type == Projectile.type // Make sure the Projectile is of the same type as this javelin
					&& currentProjectile.ModProjectile is StcikyChewingGum javelinProjectile // Use a pattern match cast so we can access the Projectile like an ExampleJavelinProjectile
					&& javelinProjectile.IsStickingToTarget // the previous pattern match allows us to use our properties
					&& javelinProjectile.TargetWhoAmI == target.whoAmI)
				{

					_stickingJavelins[currentJavelinIndex++] = new Point(i, currentProjectile.timeLeft); // Add the current Projectile's index and timeleft to the point array
					if (currentJavelinIndex >= _stickingJavelins.Length)  // If the javelin's index is bigger than or equal to the point array's length, break
						break;
				}
			}

			// Remove the oldest sticky javelin if we exceeded the maximum
			if (currentJavelinIndex >= MAX_STICKY_JAVELINS)
			{
				int oldJavelinIndex = 0;
				// Loop our point array
				for (int i = 1; i < MAX_STICKY_JAVELINS; i++)
				{
					// Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
					if (_stickingJavelins[i].Y < _stickingJavelins[oldJavelinIndex].Y)
					{
						oldJavelinIndex = i; // Remember the index of the removed javelin
					}
				}
				// Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
				Main.projectile[_stickingJavelins[oldJavelinIndex].X].Kill();
			}
		}
		private const int MAX_TICKS = 45;
		private const int ALPHA_REDUCTION = 25;

		public override void AI()
		{

			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= ALPHA_REDUCTION;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}

			if (IsStickingToTarget)
			{
				Projectile.ignoreWater = true;
				Projectile.tileCollide = false;
				const int aiFactor = 15;
				Projectile.localAI[0] += 1f;

				// Every 30 ticks, the javelin will perform a hit effect
				bool hitEffect = Projectile.localAI[0] % 30f == 0f;
				int projTargetIndex = TargetWhoAmI;
				if (Projectile.localAI[0] >= 60 * aiFactor || projTargetIndex < 0 || projTargetIndex >= 200)
				{ // If the index is past its limits, kill it
					Projectile.Kill();
				}
				else if (Main.npc[projTargetIndex].active && !Main.npc[projTargetIndex].dontTakeDamage)
				{ // If the target is active and can take damage
				  // Set the Projectile's position relative to the target's center
					Projectile.Center = Main.npc[projTargetIndex].Center - Projectile.velocity * 2f;
					Projectile.gfxOffY = Main.npc[projTargetIndex].gfxOffY;
					if (hitEffect)
					{ // Perform a hit effect here
						Main.npc[projTargetIndex].HitEffect(0, 1.0);
					}
				}
				else
				{ // Otherwise, kill the Projectile
					Projectile.Kill();
				}
			}
			else
			{
				TargetWhoAmI++;
				if (TargetWhoAmI >= MAX_TICKS)
				{
					// Change these multiplication factors to alter the javelin's movement change after reaching maxTicks
					const float velXmult = 0.9f; // x velocity factor, every AI update the x velocity will be 98% of the original speed
					const float velYmult = 1f; // y velocity factor, every AI update the y velocity will be be 0.35f bigger of the original speed, causing the javelin to drop to the ground
					TargetWhoAmI = MAX_TICKS; // set ai1 to maxTicks continuously
											  //Projectile.velocity.X *= velXmult;
					Projectile.velocity.Y += velYmult;
				}

				Projectile.rotation += Projectile.velocity.ToRotation() * 0.1f;

				// Spawn some random dusts as the javelin travels
				if (Main.rand.NextBool(4))
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.t_Slime,
						Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
					dust.velocity += Projectile.velocity * 0.3f;
					dust.velocity *= 0.2f;
					dust.noGravity = true;
				}
				if (Main.rand.NextBool(6))
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.t_SteampunkMetal,
						0, 0, 254, Scale: 0.3f);
					dust.velocity += Projectile.velocity * 0.5f;
					dust.velocity *= 0.5f;
					dust.noGravity = true;
				}
			}
		}
	}
}

