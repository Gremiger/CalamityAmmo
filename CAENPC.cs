using CalamityAmmo.Accessories;
using CalamityAmmo.Ammos.Pre_Hardmode;
using CalamityAmmo.Misc;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityAmmo.Rockets;
using CalamityMod;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.HiveMind;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.Perforator;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalamityAmmo
{
	public class CAENPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public float elementaldouble;
		public float elementaldoublemax = 16f;
		public override void AI(NPC npc)
		{
			if (elementaldouble > 0) elementaldouble--;
		}
		public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			if (elementaldouble > 0)
			{
				Color tagColor = Color.White;
				float drawtimer = (float)Main.timeForVisualEffects + 18;
				Vector2 drawpos = npc.Center + new Vector2((float)(Math.Cos(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2), (float)(Math.Sin(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2));
				Main.EntitySpriteDraw(Request<Texture2D>("CalamityAmmo/Misc/SolarTag").Value,
					drawpos - Main.screenPosition, new Rectangle(0, 0, 48, 22), tagColor, (npc.Center - drawpos).ToRotation(), new Vector2(24, 11), 1, SpriteEffects.None, 0);

				drawpos = npc.Center + new Vector2((float)(-Math.Sin(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2), (float)(Math.Cos(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2));
				Main.EntitySpriteDraw(Request<Texture2D>("CalamityAmmo/Misc/VortexTag").Value,
					drawpos - Main.screenPosition, new Rectangle(0, 0, 48, 22), tagColor, (npc.Center - drawpos).ToRotation(), new Vector2(24, 11), 1, SpriteEffects.None, 0);

				drawpos = npc.Center + new Vector2((float)(-Math.Cos(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2), (float)(-Math.Sin(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2));
				Main.EntitySpriteDraw(Request<Texture2D>("CalamityAmmo/Misc/NebulaTag").Value,
					drawpos - Main.screenPosition, new Rectangle(0, 0, 48, 22), tagColor, (npc.Center - drawpos).ToRotation(), new Vector2(24, 11), 1, SpriteEffects.None, 0);

				drawpos = npc.Center + new Vector2((float)(Math.Sin(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2), (float)(-Math.Cos(drawtimer / 24) * Math.Max(npc.width, npc.height) / 2));
				Main.EntitySpriteDraw(Request<Texture2D>("CalamityAmmo/Misc/StardustTag").Value,
					drawpos - Main.screenPosition, new Rectangle(0, 0, 48, 22), tagColor, (npc.Center - drawpos).ToRotation(), new Vector2(24, 11), 1, SpriteEffects.None, 0);
			}

		}
		public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
		{
			Player player = Main.player[projectile.owner];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (projectile.type == ModContent.ProjectileType<_DazzlingAstralArrow>() &&
					   Main.myPlayer == projectile.owner)
			{
				Vector2 spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				int star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center + npc.velocity * (600 / 24) - spawnPos) * 24f, ProjectileID.StarCannonStar, (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center + npc.velocity * (600 / 24) - spawnPos) * 24f, ProjectileID.HallowStar, (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<AstralStar>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<AncientStar>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<HadalUrnStarfish>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<LeonidStar>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<FakeSeaStar1>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
				spawnPos = npc.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
				star = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, Vector2.Normalize(npc.Center - spawnPos) * 24f, ModContent.ProjectileType<FakeSeaStar2>(), (int)(projectile.damage * 0.33f), projectile.knockBack, Main.myPlayer);
				Main.projectile[star].usesIDStaticNPCImmunity = false;
				Main.projectile[star].usesLocalNPCImmunity = true;
				Main.projectile[star].localNPCHitCooldown = 21;
				Main.projectile[star].netUpdate = true;
			}
			if (npc.type != NPCID.TargetDummy && Main.myPlayer == projectile.owner && projectile.type == ModContent.ProjectileType<_Seed>()
				&& player.ownedProjectileCounts[ModContent.ProjectileType<_Flower>()] <= 0)
			{
				Vector2 spawnPos = npc.Center - new Vector2(0, npc.height / 2);
				int proj = Projectile.NewProjectile(npc.GetSource_FromAI(), spawnPos, npc.velocity * 8, ModContent.ProjectileType<_Flower>(), projectile.damage, projectile.knockBack, Main.myPlayer);
				Main.projectile[proj].netUpdate = true;
			}
		}
	}
}

public class NPCLoot : GlobalNPC
{
	public override void ModifyNPCLoot(NPC npc, Terraria.ModLoader.NPCLoot npcLoot)
	{
		if (npc.type == ModContent.NPCType<WulfrumDrone>() ||
			npc.type == ModContent.NPCType<WulfrumGyrator>() ||
			npc.type == ModContent.NPCType<WulfrumHovercraft>() ||
			npc.type == ModContent.NPCType<WulfrumRover>() ||
			npc.type == ModContent.NPCType<WulfrumAmplifier>())
		{
			npcLoot.Add(ModContent.ItemType<WulfrumCoil>(), new Fraction(7, 100));
		}
		if (npc.type == ModContent.NPCType<Crabulon>())
		{
			LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<InfectedCrabGill>(), 5));
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MarvelousMycelium>(), 5));
			//notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MushroomMortar>(), 4));
			npcLoot.Add(notExpertRule);
		}
		if (npc.type == ModContent.NPCType<DesertScourgeHead>())
		{
			LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
			notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<SandWorm>(), 4));
			npcLoot.Add(notExpertRule);
		}
		if (npc.type == ModContent.NPCType<DesertNuisanceHead>())
		{
			npcLoot.Add(ModContent.ItemType<SandWorm>(), new Fraction(5, 100));
		}
		if (npc.type == NPCType<PerforatorHive>())
		{
			/*LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
			notExpertRule.OnSuccess(ItemDropRule.Common(ItemType<BloodBullet>(), 1,250,999));
			npcLoot.Add(notExpertRule);*/
			npcLoot.Add(ItemType<BloodBullet>(), 1, 500, 999);
		}
		if (npc.type == NPCType<HiveMind>())
		{
			npcLoot.Add(ItemType<RottenBullet>(), 1, 500, 999);
		}
		if (npc.type == NPCID.QueenBee)
		{
			LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());
			//notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BeenadeLauncher>(), 4));
			//npcLoot.Add(notExpertRule);
		}
		if (npc.type == NPCID.Vulture)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DesertFeather>(), 1, 1, 2));
		}
	}
}
public class ArmsDealer : GlobalNPC
{
	public override bool AppliesToEntity(NPC npc, bool lateInstatiation)
	{
		return npc.type == NPCID.ArmsDealer;
	}
	public override void ModifyShop(NPCShop shop)
	{
		if (shop.NpcType == NPCID.ArmsDealer)
		{
			var DownedAnyCalamityEvilBoss = new Condition("Conditions.DownedAnyCalamityEvilBoss", () => DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator);
			shop.Add(ModContent.ItemType<FastHolster>(), DownedAnyCalamityEvilBoss);
		}
	}


}
public class Demolitionist : GlobalNPC
{
	public override bool AppliesToEntity(NPC npc, bool lateInstatiation)
	{
		return npc.type == NPCID.Demolitionist;
	}
	public override void ModifyShop(NPCShop shop)
	{
		if (shop.NpcType == NPCID.Demolitionist)
		{
			var DownedDesertScourge = new Condition("Conditions.DownedDesertScourge", () => DownedBossSystem.downedDesertScourge);
			shop.Add(ModContent.ItemType<SandRocket>(), DownedDesertScourge);
			var DownedAnyCalamityEvilBoss = new Condition("Conditions.DownedAnyCalamityEvilBoss", () => DownedBossSystem.downedHiveMind || DownedBossSystem.downedPerforator);
			shop.Add(ModContent.ItemType<Aerocket>(), DownedAnyCalamityEvilBoss);
		}
	}

}





