using CalamityAmmo.Accessories;
using CalamityAmmo.CAESystem;
using CalamityMod;
using CalamityMod.Buffs.Alcohol;
using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo
{
	public class CaePlayer : ModPlayer
	{
		public bool Stars;
		public bool Zip
		{
			get;
			set;
		}
		public bool Aimed
		{
			get;
			set;
		}
		public bool Coil;
		public bool Coil2;
		public bool Coil3;
		public bool Coil4;
		public bool Spore;
		public bool Radio;
		public bool Holster;
		public bool Evil;
		public bool Odd;
		public bool MUN;
		public bool Live;
		public bool Arcane;
		public bool Arcane2;
		public bool KnowledgeAuric;
		public bool LowATKspeed;
		public bool Grape;
		public float atkspeed = 0f;
		public int HealCD = 0;
		public int GrapeTime2 = 0;
		public bool Odd2;
		public int arcaneQuiverManaCost = 0;
		public bool icyCoil;
		public int icyBombCD;
		public bool hotCoil;
		public int hotBombCount;
		public bool electricCoil;
		public bool autoCoil;
		public int autoSelectMode;
		public bool victide;
		public bool Mycelium
		{
			get;
			set;
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
		{

			if (CAEKeybinds.OddHotKey.JustPressed && Odd2)
			{
				Odd = !Odd;
			}
			if (CAEKeybinds.autoCoilKey.JustPressed && autoCoil)
			{
				autoSelectMode++;
				//Main.NewText("autoSelectMode" + autoSelectMode);
				if (autoSelectMode >= 7)
				{
					autoSelectMode = 0;
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.selectOff"));
				}
				if (autoSelectMode == 1)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.selectNearPlayer"));
				}
				if (autoSelectMode == 2)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.selectNearMouse"));
					//CombatText.UpdateCombatText();
				}
				if (autoSelectMode == 3)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.selectLowLife"));

				}
				if (autoSelectMode == 4)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.selectLowLP"));
				}
				if (autoSelectMode == 5)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.sld"));
				}
				if (autoSelectMode == 6)
				{
					CombatText.NewText(Player.Hitbox, Color.YellowGreen, Language.GetTextValue("Mods.CalamityAmmo.shd"));
				}
			}

		}
		public override void PreUpdate()
		{
			CaePlayer modplayer = Player.GetModPlayer<CaePlayer>();

		}

		public override void PostUpdate()
		{
			CaePlayer modplayer = Player.GetModPlayer<CaePlayer>();
			//Main.NewText("canArcaneTransform = " + CAEGlobalItem.manaCost);
			if (icyBombCD > 0) icyBombCD--;
			if (Holster)
			{
				Player.GetCritChance<RangedDamageClass>() -= 4;
			}
			if (Evil)
			{
				Player.AddBuff(ModContent.BuffType<MarkedforDeath>(), 2);
			}
			if (Odd && Odd2)
			{
				Player.AddBuff(ModContent.BuffType<Trippy>(), 2, false, false);
			}
			if (Mycelium)
			{
				if (!Player.HasBuff(ModContent.BuffType<Mushy>()))
				{
					if (MUN)
					{
						Player.GetDamage<RangedDamageClass>() *= 1.15f;
						Player.GetCritChance<RangedDamageClass>() += 5f;
					}
					else
					{
						Player.GetDamage<RangedDamageClass>() += 0.06f;
						Player.GetCritChance<RangedDamageClass>() += 9f;
					}
					Player.statDefense -= 5;
					Player.lifeRegen -= 1;
				}

			}
			if (Spore)
			{
				modplayer.SporeSac();
				Player.lifeRegen -= 1;
			}
			if (MUN)
			{
				Player.aggro -= 500;
				if (Player.ownedProjectileCounts[ModContent.ProjectileType<Shroomere>()] <= 0)
				{
					Vector2 spawnPos = Player.Center;
					int shroomer = Projectile.NewProjectile(Player.GetSource_Accessory(new(ModContent.ItemType<MushroomUnitedNations>())),
					   spawnPos, new Vector2(0, 0), ModContent.ProjectileType<Shroomere>(),
						(int)Player.GetDamage<RangedDamageClass>().ApplyTo(150f), 1f, Main.myPlayer);
					Main.projectile[shroomer].CritChance = 39 + (int)Player.GetCritChance<RangedDamageClass>();
				}
			}
			/*if (Live)
			{
				HealCD++;
				if (HealCD >= 60 * 6 && Player.ownedProjectileCounts[ModContent.ProjectileType<HealOrb>()] <= 0)
				{
					Projectile.NewProjectile(Player.GetSource_Accessory(null),
					Player.Center + new Vector2(Main.rand.Next(-200, 201), Main.rand.Next(-242, -10)),
					new Vector2(0, 0),
					ModContent.ProjectileType<HealOrb>(), 0, 0,
					Player.whoAmI, 15);
					HealCD = 0;
				}
			}*/
			if (Player.Calamity().gloveOfRecklessness)
			{
				//Player.GetAttackSpeed<MeleeDamageClass>() += 150f;
				Player.GetDamage<RangedDamageClass>() -= 0.10f;
				//Player.GetCritChance<RangedDamageClass>() -= 10f;
			}
			if (Player.Calamity().gloveOfPrecision)
			{
				//Player.GetAttackSpeed<RangedDamageClass>() -= 0.15f;
				Player.GetDamage<RangedDamageClass>() += 0.10f;
				Player.GetCritChance<RangedDamageClass>() += 10f;
			}
			if (Coil3)
			{
				Player.GetCritChance<RangedDamageClass>() += 7f + 24f / Player.HeldItem.useTime;
			}
			if (Coil4)
			{
				Player.GetDamage<RangedDamageClass>() *= 1.07f + Player.HeldItem.useTime / 480f;
			}
			if(victide)
			{
				Player.GetDamage(DamageClass.Ranged).Flat += 1f;
				Player.GetCritChance(DamageClass.Ranged) += 1f;
				Player.endurance += 0.01f;
				Player.GetKnockback(DamageClass.Ranged).Flat += 1f;
				Player.statDefense += 1;
				Player.jumpSpeedBoost += 1f;
				Player.maxFallSpeed += 1f;
				Player.moveSpeed += 0.01f;
				Player.maxRunSpeed += 0.01f;
				Player.lifeRegen += 2;
				Player.runAcceleration += 0.01f;
				Player.GetAttackSpeed(DamageClass.Ranged) += 0.01f;
			}
		}

		public override void ResetEffects()
		{
			Stars = false;
			Aimed = false;
			Zip = false;
			Coil = false;
			Coil2 = false;
			Coil3 = false;
			Spore = false;
			Mycelium = false;
			Radio = false;
			Holster = false;
			Evil = false;
			MUN = false;
			Live = false;
			Arcane = false;
			Arcane2 = false;
			Coil4 = false;
			LowATKspeed = false;
			Grape = false;
			Odd2 = false;
			autoCoil = false;
			electricCoil = false;
			hotCoil = false;
			icyCoil = false;
			victide = false;
		}
		public override void UpdateDead()
		{
			Stars = false;
			Aimed = false;
			Zip = false;
			Coil = false;
			Coil2 = false;
			Coil3 = false;
			Spore = false;
			Mycelium = false;
			Radio = false;
			Holster = false;
			Evil = false;
			MUN = false;
			Live = false;
			Arcane = false;
			Arcane2 = false;
			Coil4 = false;
			LowATKspeed = false;
			Grape = false;
			Odd2 = false;
			victide = false;
		}
		public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
		{
			if (Player.active && !Player.dead && Player.whoAmI == Main.myPlayer && Aimed)
			{
				Vector2 start = Player.MountedCenter + new Vector2(0, 0);
				Vector2 end = Main.MouseWorld;
				if (start != end)
				{
					float length = Vector2.Distance(start, end);
					Vector2 direction = end - start;
					direction.Normalize();
					Texture2D texture = ModContent.Request<Texture2D>("CalamityAmmo/Misc/AimLine").Value;
					Texture2D texture1 = ModContent.Request<Texture2D>("CalamityAmmo/Misc/AimStar").Value;

					for (float k = 0; k <= length; k += 16f)
					{
						Vector2 drawlight = start + k * direction + new Vector2(6, 6);
						Color color = Lighting.GetColor((int)(drawlight.X / 16), (int)(drawlight.Y / 16));
						color *= 1f;

						Main.EntitySpriteDraw(texture, start + k * direction - Main.screenPosition, null, color, 0, texture.Size() / 2, 0.7f, SpriteEffects.None, 0);
					}
					float rotation = 0f;
					//rotation += Player.itemTime;
					//rotation +=Player.ItemAnimationActive? (float)( 90f * Main.timeForVisualEffects/(100f*Player.itemAnimationMax) ):0f;
					Main.EntitySpriteDraw(texture1, Main.MouseScreen, null, Color.White, rotation, texture1.Size() / 2, (float)(1f + 0.07f * Math.Sin(Main.timeForVisualEffects / 20f)), SpriteEffects.None, 0); ;
				}
			}
			base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
		}


		public override void PostHurt(Player.HurtInfo info)
		{
			if (Mycelium)
			{
				Player.AddBuff(ModContent.BuffType<Mushy>(), 300);
				Player.GetDamage<RangedDamageClass>() -= 0.06f;
				Player.GetCritChance<RangedDamageClass>() -= 9f;
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{

		}
		public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Item, consider using OnHitNPC instead */
		{

		}
		public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)/* tModPorter If you don't need the Projectile, consider using OnHitNPC instead */
		{

		}

		public void SporeSac()
		{
			if (Main.rand.NextBool(15))
			{
				int totalprojectiles = 0;
				for (int i = 0; i < 1000; i++)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == Player.whoAmI && (Main.projectile[i].type == ModContent.ProjectileType<Crabulon_Spore>()))
					{
						totalprojectiles++;
					}
				}
				if (Main.rand.Next(15) >= totalprojectiles && totalprojectiles < 10)
				{
					int num3 = 24;
					int num4 = 90;
					for (int j = 0; j < 50; j++)
					{
						int num5 = Main.rand.Next(200 - j * 2, 400 + j * 2);
						Vector2 center = Player.Center;
						center.X += Main.rand.Next(-num5, num5 + 1);
						center.Y += Main.rand.Next(-num5, num5 + 1);
						if (!Collision.SolidCollision(center, num3, num3) && !Collision.WetCollision(center, num3, num3))
						{
							center.X += num3 / 2;
							center.Y += num3 / 2;
							if (Collision.CanHit(new Vector2(Player.Center.X, Player.position.Y), 1, 1, center, 1, 1) || Collision.CanHit(new Vector2(Player.Center.X, Player.position.Y - 50f), 1, 1, center, 1, 1))
							{
								int num6 = (int)center.X / 16;
								int num7 = (int)center.Y / 16;
								bool flag = false;
								if (Main.rand.NextBool(3))
								{
									flag = true;
								}
								else
								{
									center.X -= (num4 / 2);
									center.Y -= (num4 / 2);
									if (Collision.SolidCollision(center, num4, num4))
									{
										center.X += num4 / 2;
										center.Y += num4 / 2;
										flag = true;
									}
								}
								if (flag)
								{
									for (int k = 0; k < 1000; k++)
									{
										if (Main.projectile[k].active && Main.projectile[k].owner == Player.whoAmI && Main.projectile[k].aiStyle == 105 && (center - Main.projectile[k].Center).Length() < 48f)
										{
											flag = false;
											break;
										}
									}
									if (flag && Main.myPlayer == Player.whoAmI)
									{
										int damage = 10;
										if (MUN)
										{
											damage = 50;
										}
										int proj = Projectile.NewProjectile(Projectile.GetSource_None(), center, new Vector2(0, 0), ModContent.ProjectileType<Crabulon_Spore>(),
											damage, 1.5f, Player.whoAmI);
										Main.projectile[proj].usesLocalNPCImmunity = true;
										Main.projectile[proj].localNPCHitCooldown = 15;
										return;
									}
								}
							}
						}

					}
				}
			}
		}
		public override void PostUpdateMiscEffects()
		{
			//OddMushroomEffects();
		}
	}
}


