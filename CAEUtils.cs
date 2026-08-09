using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using Terraria;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace CalamityAmmo
{
	internal static class CAEUtils
	{
		public static NPC NPCExists(float whoAmI, params int[] types)
		{
			return NPCExists((int)whoAmI, types);
		}
		public static int FindClosestHostileNPC(Vector2 location, float detectionRange, bool lineCheck = false)
		{
			NPC closestNpc = null;
			foreach (NPC n in Main.npc)
			{
				if (n.CanBeChasedBy() && n.Distance(location) < detectionRange && (!lineCheck || Collision.CanHitLine(location, 0, 0, n.Center, 0, 0)))
				{
					detectionRange = n.Distance(location);
					closestNpc = n;
				}
			}
			return closestNpc == null ? -1 : closestNpc.whoAmI;
		}
		public static bool HasAccessoryEquipped(Player player, int itemType)
		{
			for (int i = 3; i <= 12; i++)
			{
				if (player.armor[i].type == itemType)
					return true;
			}
			return false;
		}
		public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
		{
			float colorMePurple = (float)((Math.Sin((double)(Math.PI * 2 / seconds) * Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
			return Color.Lerp(firstColor, secondColor, colorMePurple);
		}
		public static void autoSelectNPC(Player player, Projectile projectile, bool ignoreTiles, float distanceRequired, float homingVelocity, float N)
		{
			if (projectile.hostile || projectile.numHits > 0)
				return;
			Vector2 targetPos = projectile.Center;
			int tarWho = -1;
			int autoSelectMode = player.GetModPlayer<CaePlayer>().autoSelectMode;

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];

				float num = npc.width / 2 + npc.height / 2;
				if (npc.CanBeChasedBy(projectile, true) && (Vector2.Distance(npc.Center, projectile.Center) < distanceRequired + num) && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, Main.npc[i].Center, 1, 1)))
				{
					if (autoSelectMode == 1)
					{
						if ((targetPos == projectile.Center)
						|| (Vector2.Distance(targetPos, player.Center) > Vector2.Distance(npc.Center, player.Center)))
						{
							targetPos = npc.Center;

						}
					}
					if (autoSelectMode == 3)
					{
						if ((tarWho == -1)
						|| (Main.npc[tarWho].life > npc.life))
						{
							tarWho = i;
							targetPos = Main.npc[tarWho].Center;
						}
					}
					if (autoSelectMode == 4)
					{
						if ((tarWho == -1)
						|| ((Main.npc[tarWho].GetLifePercent() > npc.GetLifePercent())&&npc.GetLifePercent()<1f))
						{

							tarWho = i;
							targetPos = Main.npc[tarWho].Center;
							//Main.NewText("tarWho=" + Main.npc[tarWho]);
							//Main.NewText("i=" + Main.npc[i]);
							//Main.NewText("Main.npc[tarWho].GetLifePercent()=" + Main.npc[tarWho].GetLifePercent());
							//Main.NewText("npc.GetLifePercent()=" + npc.GetLifePercent());
						}
					}
					if (autoSelectMode == 2)
					{
						if ((targetPos == projectile.Center)
						|| (Vector2.Distance(targetPos - Main.screenPosition, Main.MouseScreen) > Vector2.Distance(npc.Center - Main.screenPosition, Main.MouseScreen)))
						{
							//Main.NewText("Main.MouseScreen=" + Main.MouseScreen);
							//Main.NewText("targetPos=" + (targetPos - Main.screenPosition));
							//Main.NewText("Vector2.Distance(targetPos, Main.MouseScreen)=" + Vector2.Distance(targetPos, Main.MouseScreen));
							targetPos = npc.Center;
						}
					}
					if (autoSelectMode == 5)
					{
						if ((tarWho == -1)
						|| (Main.npc[tarWho].defense> npc.defense))
						{
							tarWho = i;
							targetPos = Main.npc[tarWho].Center;
						}
					}
					if (autoSelectMode == 6)
					{
						if ((tarWho == -1)
						|| (Main.npc[tarWho].defense < npc.defense))
						{
							tarWho = i;
							targetPos = Main.npc[tarWho].Center;
						}
					}
				}
			}
			if (targetPos != projectile.Center)
			{
				Vector2 vector = (targetPos - projectile.Center).SafeNormalize(Vector2.UnitY);
				projectile.velocity = (projectile.velocity * N + vector * homingVelocity) / (N + 1f);
			}
		}
		public static List<Vector2> DrawArc(Vector2 start, Vector2 end, float radius, float angle)
		{
			List<Vector2> arcPoints = new List<Vector2>();

			float centerX = (start.X + end.X) / 2;
			float centerY = (start.Y + end.Y) / 2;

			float angleRadians = (float)(angle * (Math.PI / 180));
			float startAngle = (float)Math.Atan2(start.Y - centerY, start.X - centerX);
			float endAngle = startAngle + angleRadians;

			for (float t = 0; t <= 2; t += 0.01f)
			{
				float currentAngle = startAngle + t * angleRadians;
				float x = (float)(centerX + radius * Math.Cos(currentAngle));
				float y = (float)(centerY + radius * Math.Sin(currentAngle));
				arcPoints.Add(new Vector2(x, y));
			}

			return arcPoints;
		}
		
		//public static string Language.GetTextValue(string key,bool  caeKey=true)=>Language.GetOrRegister(caeKey?$"Mods.CalamityAmmo.{key}":key).get(Language.ActiveCulture);
	}
}
