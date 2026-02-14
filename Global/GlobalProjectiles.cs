using CalamityAmmo.Accessories;
using CalamityAmmo.Ammos.Post_MoonLord;
using CalamityAmmo.Projectiles.GenerateByAccessories;
using CalamityAmmo.Rockets;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace CalamityAmmo.Global
{
	public class GlobalProjectiles : GlobalProjectile
	{

		public override bool InstancePerEntity => true;
		public override void SetDefaults(Projectile projectile)
		{
			Player player = Main.player[projectile.owner];

		}
		public override bool PreDraw(Projectile projectile, ref Color lightColor)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			/*if(modplayer.Arcane2&&projectile.arrow)
			{
				Vector2 position = projectile.Center - Main.screenPosition;
				Texture2D texture = ModContent.Request<Texture2D>("CalamityAmmo/Accessories/ArcaneArrow_Proj").Value;
				Rectangle sourceRectangle = texture.Frame(); // The sourceRectangle says which frame to use.
				Vector2 origin = sourceRectangle.Size() / 2f;
				float scale = projectile.scale * 1f;
				SpriteEffects spriteEffects = ((!(projectile.ai[0] >= 0f)) ? SpriteEffects.FlipVertically : SpriteEffects.None); // Flip the sprite based on the direction it is facing.

				float lightingColor = Lighting.GetColor(projectile.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3.0);
				lightingColor = Utils.Remap(lightingColor, 0.2f, 1f, 0f, 1f);
				//Main.EntitySpriteDraw(texture, position, sourceRectangle, Color.White, projectile.velocity.ToRotation()+1.57f, origin, scale/2, spriteEffects, 0f);
				// Very faint part affected by the light color
				//return false;
			}*/
			return true;
		}
		public override void DrawBehind(Projectile projectile, int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
		{

			base.DrawBehind(projectile, index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
		}
		public override bool PreAI(Projectile projectile)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (modplayer.autoCoil && modplayer.autoSelectMode > 0)
			{
				if (projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<RicoshotCoin>())
				{
					CAEUtils.autoSelectNPC(player, projectile, !projectile.tileCollide, 960f,
					projectile.velocity.Length() > 8f ? projectile.velocity.Length() : 10f, 5f);
				}
			}
			return true;
		}

		public override void AI(Projectile projectile)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();

			if (modplayer.Radio && player.heldProj != projectile.whoAmI)
			{
				foreach (var proj in Main.projectile)
					if (proj.type != ModContent.ProjectileType<TeslaAura>() && proj.CountsAsClass<RangedDamageClass>())
					{
						int Proj = Projectile.NewProjectile(projectile.GetSource_FromThis(), proj.Center, proj.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);
						Main.projectile[Proj].Center = proj.Center;
						Main.projectile[Proj].velocity = proj.velocity;
						Main.projectile[Proj].timeLeft = 1;
					}
			}
			if (//player.HeldItem.type == ModContent.ItemType<BeenadeLauncher>() ||
			player.HeldItem.type == ModContent.ItemType<PlaguenadeLauncher>())
			{
				if (projectile.type == ModContent.ProjectileType<PlaguenadeProj>() && projectile.owner == Main.myPlayer)
				{
					projectile.DamageType = DamageClass.Ranged;
					for (int i = 0; i < 200; i++)
					{
						if (Main.npc[i].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1))
						{
							float distanceFromTarget = projectile.Center.ManhattanDistance(Main.npc[i].Center);
							float Linghuodistance = 128f;
							if (distanceFromTarget < Linghuodistance)
							{
								if (projectile.owner == Main.myPlayer)
								{
								}
								SoundEngine.PlaySound(SoundID.Item14, new Vector2?(projectile.position));
								projectile.Kill();
								//Main.NewText(projectile.timeLeft);
							}
						}

						//Main.NewText(projectile.timeLeft);
					}
				}
			}
		}
		public override void OnSpawn(Projectile projectile, IEntitySource source)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI && projectile.type != ModContent.ProjectileType<RicoshotCoin>())
			{
				if (modplayer.Coil)
				{
					projectile.velocity *= 1.08f;
				}
				if (modplayer.Coil2)
				{
					projectile.velocity *= 1.2f;
					if (Main.rand.NextBool(144))
					{
						player.AddBuff(144, 45);
					}
				}
				if (modplayer.Coil3 || modplayer.Coil4)
				{
					projectile.velocity *= 1.33f;
				}
				if (modplayer.icyCoil)
				{
					projectile.velocity *= 1.2f;
				}
				if (modplayer.hotCoil)
				{
					projectile.velocity *= 1.2f;
				}
				if (modplayer.autoCoil)
				{
					projectile.velocity *= 1.2f;
				}
				if (modplayer.electricCoil)
				{
					projectile.velocity *= 1.4f;
				}
			}
			if (modplayer.Radio && player.heldProj != projectile.whoAmI)
			{
				if (projectile.type != ModContent.ProjectileType<TeslaAura>() && projectile.CountsAsClass<RangedDamageClass>())
				{
					Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<TeslaAura>(), (int)(projectile.damage * 0.2f), 0f, player.whoAmI);
				}
			}
			if((projectile.arrow || projectile.type == ModContent.ProjectileType<MirageArrow_Proj2>())
				&& projectile.type != ModContent.ProjectileType<ArcaneArrow_Proj>()
				&& projectile.type != ProjectileID.PhantasmArrow)
			{
				if (player.CheckMana(CAEGlobalItem.manaCost))
				{
					if (modplayer.Arcane)
					{
						int damage = projectile.damage;
						if (player.statMana / (float)player.statManaMax2 >= 0.75f)
						{
							damage += (int)player.GetDamage<RangedDamageClass>().ApplyTo(15f);
						}
						else if (player.statMana / (float)player.statManaMax2 >= 0.5f)
						{
							damage += (int)player.GetDamage<RangedDamageClass>().ApplyTo(10f);
						}
						else if (player.statMana / (float)player.statManaMax2 >= 0.25f)
						{
							damage += (int)player.GetDamage<RangedDamageClass>().ApplyTo(5f);
						}
						if (player.HasBuff(BuffID.ManaSickness))
						{
							damage /= 2;
						}
						projectile.damage = damage;
						projectile.netUpdate = true;
						if (modplayer.Arcane2)
						{
							projectile.penetrate = 0;
							projectile.active = false;
							Projectile proj = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, projectile.velocity, ModContent.ProjectileType<ArcaneArrow_Proj>(), damage, projectile.knockBack, player.whoAmI);
							proj.CritChance = (int)player.GetCritChance<RangedDamageClass>();
							proj.netImportant = true;
						}
					}
				}
				//Main.NewText((float)player.statMana / (float)player.statManaMax2);
			}
			if (projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.AuricBullet>()
			&& player.controlUseItem)
			{
				if (Vector2.Distance(projectile.Center, player.Center) <= 32f)
				{
					projectile.Center = player.HeldItem.width >= 58f ? player.Center + projectile.velocity.SafeNormalize(Vector2.One)
					* player.HeldItem.width : player.Center + projectile.velocity.SafeNormalize(Vector2.One) * 58f;
				}
			}

		}
		public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI)
			{
				if (modplayer.Coil2)
				{
					if (Main.rand.NextBool(14))
					{
						target.AddBuff(144, 45);
					}
				}
				if (modplayer.icyCoil)
				{
					target.AddBuff(BuffID.Frostburn2, 60);
					if (projectile.type != ModContent.ProjectileType<IceBomb>() &&
					projectile.type != ModContent.ProjectileType<IceRain>())
					{
						if (modplayer.icyBombCD == 0)
						{
							float sideLength = target.width > target.height ? target.width * 2f / 3f : target.height * 2f / 3f;
							Vector2 pos1 = target.Center + new Vector2(sideLength, sideLength);
							Vector2 pos2 = target.Center + new Vector2(-sideLength, sideLength);
							Vector2 pos3 = target.Center + new Vector2(-sideLength, -sideLength);
							Vector2 pos4 = target.Center + new Vector2(sideLength, -sideLength);
							Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), pos1, -(target.Center - pos1).SafeNormalize(Vector2.Zero), ModContent.ProjectileType<IceBomb>(), projectile.damage / 3, projectile.knockBack, player.whoAmI);
							Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), pos2, -(target.Center - pos2).SafeNormalize(Vector2.Zero), ModContent.ProjectileType<IceBomb>(), projectile.damage / 3, projectile.knockBack, player.whoAmI);
							Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), pos3, -(target.Center - pos3).SafeNormalize(Vector2.Zero), ModContent.ProjectileType<IceBomb>(), projectile.damage / 3, projectile.knockBack, player.whoAmI);
							Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), pos4, -(target.Center - pos4).SafeNormalize(Vector2.Zero), ModContent.ProjectileType<IceBomb>(), projectile.damage / 3, projectile.knockBack, player.whoAmI);
							modplayer.icyBombCD = 300;
						}
					}
				}
				if (modplayer.hotCoil)
				{
					target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 30);
					if (projectile.numHits <= 0 && projectile.type != ModContent.ProjectileType<BrimstoneHellblast>()) modplayer.hotBombCount++;
					if (modplayer.hotBombCount >= 6)
					{

						float projectileSpeed = 1f;//death ? 9f : (revenge ? 8f : 6f);
						projectileSpeed += 1.5f;// * enrageScale;
						Vector2 projectileVelocity2 = projectile.velocity.SafeNormalize(Vector2.Zero) * projectileSpeed;
						float radialOffset = 0.2f;
						float diameter = 48f;
						//projectileVelocity2 = Utils.SafeNormalize(projectileVelocity2, Vector2.UnitY) * projectileSpeed;
						Vector2 velocity2 = projectileVelocity2;
						velocity2 = Utils.SafeNormalize(velocity2, Vector2.UnitY);
						velocity2 *= diameter;
						int totalProjectiles = 6;
						float offsetAngle = 3.1415927f * radialOffset;
						int type3 = ModContent.ProjectileType<BrimstoneHellblast>();
						int damage3 = (int)player.GetDamage(DamageClass.Ranged).ApplyTo(24);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							for (int n = 0; n < modplayer.hotBombCount; n++)
							{
								float radians = n - (totalProjectiles - 1f) / 2f;
								Vector2 offset = Utils.RotatedBy(velocity2, (double)(offsetAngle * radians), default(Vector2));
								Projectile.NewProjectile(player.GetSource_FromAI(null), player.MountedCenter + offset + Vector2.UnitX * player.direction * -24, projectileVelocity2, type3, damage3, 0f, Main.myPlayer, 1f, 0f, 0f);
							}
						}
						modplayer.hotBombCount = 0;
					}
				}
				if (modplayer.electricCoil)
				{
					target.AddBuff(BuffID.Electrified, 45);
					if (!TransformerCoil.HitNPC.Contains(target.whoAmI)) TransformerCoil.HitNPC.Add(target.whoAmI);
					if (TransformerCoil.HitNPC.Count > 2)
					{
						TransformerCoil.HitNPC.RemoveAt(0);
					}
					if (TransformerCoil.HitNPC.Count == 2)
					{
						if (Vector2.Distance(Main.npc[TransformerCoil.HitNPC[1]].Center, Main.npc[TransformerCoil.HitNPC[0]].Center)
						<= 1440)
							if (target.whoAmI == TransformerCoil.HitNPC[0])
							{
								//Main.npc[TransformerCoil.HitNPC[1]].SimpleStrikeNPC(damageDone / 2,
								//hit.HitDirection * -1);
								Vector2 toTarget = Main.npc[TransformerCoil.HitNPC[1]].Center - target.Center;
								toTarget.Normalize();
								toTarget *= 6f;
								toTarget = toTarget.RotatedBy(Main.rand.NextFloatDirection() * 0.4f);
								Projectile.NewProjectile(player.GetSource_FromThis("eleCoil"), target.Center + target.velocity * 4f,
											toTarget, ModContent.ProjectileType<ElectricStream>(), damageDone / 3, 5f, projectile.owner, Main.npc[TransformerCoil.HitNPC[1]].whoAmI);
							}
						if (target.whoAmI == TransformerCoil.HitNPC[1])
						{
							//Main.npc[TransformerCoil.HitNPC[0]].SimpleStrikeNPC(damageDone / 2,
							//hit.HitDirection * -1);
							Vector2 toTarget = Main.npc[TransformerCoil.HitNPC[0]].Center - target.Center;
							toTarget.Normalize();
							toTarget *= 6f;
							toTarget = toTarget.RotatedBy(Main.rand.NextFloatDirection() * 0.4f);
							Projectile.NewProjectile(player.GetSource_FromThis("eleCoil"), target.Center + target.velocity * 4f,
										toTarget, ModContent.ProjectileType<ElectricStream>(), damageDone / 3, 5f, projectile.owner, Main.npc[TransformerCoil.HitNPC[0]].whoAmI);
						}

					}
				}
			}
		}
		public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (projectile.CountsAsClass<RangedDamageClass>() && player.heldProj != projectile.whoAmI)
			{
				if (modplayer.icyCoil)
				{
					if (projectile.coldDamage)
					{
						modifiers.FinalDamage *= 1.1f;
					}
				}
			}
		}


	}
}











