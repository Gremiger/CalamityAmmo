using CalamityAmmo.Accessories;
using CalamityAmmo.CAESystem;
using CalamityAmmo.Misc;
using CalamityAmmo.Rockets;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.TreasureBags;
using CalamityMod.Items.TreasureBags.MiscGrabBags;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Global
{
	// Here is a class dedicated to showcasing Send/ReceiveExtraAI()
	
	public class CAEGlobalItem : GlobalItem
	{
		public static bool canArcaneTransform;
		public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
		{
			if(ItemGroups.coils.Contains(equippedItem.type)&& ItemGroups.coils.Contains(incomingItem.type))
			{
				return false;
			}
			return base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
		}
		public override void SetDefaults(Item item)
		{
			if (item.type == ItemID.Beenade ||
				item.type == ItemID.Grenade ||
				item.type == ItemID.BouncyGrenade ||
				item.type == ItemID.StickyGrenade ||
				item.type == ItemID.PartyGirlGrenade ||
				item.type == ModContent.ItemType<Plaguenade>())
			{
				item.ammo = ItemID.Beenade;
			}
		}
		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			Player player = Main.player[Main.myPlayer];
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			if (item.type == ModContent.ItemType<GloveOfRecklessness>())
			{
				tooltips.Add(new TooltipLine(Mod, "GORtooltip", Language.GetTextValue("Mods.CalamityAmmo.Glove")));
			}
			if (item.type == ModContent.ItemType<GloveOfPrecision>())
			{
				tooltips.Add(new TooltipLine(Mod, "GOPtooltip", Language.GetTextValue("Mods.CalamityAmmo.Glove")));
				//tooltips.Insert(5, new TooltipLine(Mod, "GOPtooltip", Language.GetTextValue("Glove")));
				//tooltips.Find(line => line.Name == "GOPtooltip");
			}
			if(item.useAmmo==AmmoID.Arrow&&modplayer.Arcane)
			{
				int index = tooltips.FindIndex(tip => tip.Name.StartsWith("Knockback"));
				TooltipLine mana = new TooltipLine(Mod, "Mana", Language.GetTextValue("CommonItemTooltip.UsesMana", (int)(item.useTime/3f)));
				tooltips.Insert(index+1,mana);
			}
		}
		public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();

			if (item.DamageType == DamageClass.Ranged && modplayer.Spore)
			{
				if (Main.rand.NextBool(8))
				{
					Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FungiOrb>(), (int)(damage * 0.3f), 0f, player.whoAmI);
				}
			}
			return true;
		}
		public override void UpdateInventory(Item item, Player player)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			
		}
		public override float UseSpeedMultiplier(Item item, Player player)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			CalamityPlayer calamityPlayer = player.Calamity();
			if (calamityPlayer.gloveOfRecklessness && item.DamageType == DamageClass.Ranged)
			{
				return 0.85f;
			}
			if (modplayer.Holster && item.useAmmo == AmmoID.Bullet)
			{
				return 1.1f;
			}
			if (modplayer.LowATKspeed)
			{
				return 0.000005f;
			}
			return base.UseSpeedMultiplier(item, player);
		}
		public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			CalamityPlayer calamityPlayer = player.Calamity();
			
		}
		public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			modplayer.arrowManaCost = (int)(weapon.useTime / 3f* player.manaCost);
			if (weapon.useAmmo == AmmoID.Arrow&&modplayer.Arcane&&player.statMana>=modplayer.arrowManaCost) {
				player.CheckMana(modplayer.arrowManaCost, true);
				if (player.ItemAnimationActive)player.manaRegenDelay = weapon.useTime * 2 + 1;
			}
		}
	}
	public class BossBag : GlobalItem
	{
		public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
		{
			if (item.type == ModContent.ItemType<CrabulonBag>())
			{
				itemLoot.Add(ModContent.ItemType<InfectedCrabGill>(), 4, 1, 1);
				itemLoot.Add(ModContent.ItemType<MarvelousMycelium>(), 4, 1, 1);
				//itemLoot.Add(ModContent.ItemType<MushroomMortar>(), 3, 1, 1);
			}
			if (item.type == ModContent.ItemType<StarterBag>())
			{
				itemLoot.Add(ModContent.ItemType<HardTackChest>(), 1, 1, 1);
			}
			if (item.type == ModContent.ItemType<DesertScourgeBag>())
			{
				itemLoot.Add(ModContent.ItemType<SandWorm>(), 3, 1, 1);
			}
			if (item.type == ItemID.QueenBeeBossBag)
			{
				//itemLoot.Add(ModContent.ItemType<BeenadeLauncher>(), 3, 1, 1);
			}
		}
	}
	public class ThePackRework : GlobalItem
	{
		public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 shootVel = velocity;
			int rockettype = 0;
			if (player.HeldItem.type == ModContent.ItemType<ThePack>())
			{
				switch (source.AmmoItemIdUsed)
				{
					case ItemID.RocketI: rockettype = ModContent.ProjectileType<ThePack_RocketI>(); break;
					case ItemID.RocketII: rockettype = ModContent.ProjectileType<ThePack_RocketII>(); break;
					case ItemID.RocketIII: rockettype = ModContent.ProjectileType<ThePack_RocketIII>(); break;
					case ItemID.RocketIV: rockettype = ModContent.ProjectileType<ThePack_RocketIV>(); break;
					case ItemID.ClusterRocketI: rockettype = ModContent.ProjectileType<ThePack_ClusterRocketI>(); break;
					case ItemID.ClusterRocketII: rockettype = ModContent.ProjectileType<ThePack_ClusterRocketII>(); break;

					case ItemID.MiniNukeI: rockettype = ModContent.ProjectileType<ThePack_MiniNukeRocketI>(); break;
					case ItemID.MiniNukeII: rockettype = ModContent.ProjectileType<ThePack_MiniNukeRocketII>(); break;

					case ItemID.WetRocket: rockettype = ModContent.ProjectileType<ThePack_WetRocket>(); break;
					case ItemID.LavaRocket: rockettype = ModContent.ProjectileType<ThePack_LavaRocket>(); break;
					case ItemID.HoneyRocket: rockettype = ModContent.ProjectileType<ThePack_HoneyRocket>(); break;
					case ItemID.DryRocket: rockettype = ModContent.ProjectileType<ThePack_DryRocket>(); break;
				}
				if (source.AmmoItemIdUsed == ModContent.ItemType<SandRocket>()) rockettype = ModContent.ProjectileType<ThePack_SandRocket>();
				Projectile.NewProjectile(source, position, shootVel, rockettype, damage, knockback, player.whoAmI);
				return false;
			}

			return true;


		}
	}

}