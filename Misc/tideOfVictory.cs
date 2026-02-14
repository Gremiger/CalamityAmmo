using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Misc
{
	public class tideOfVictory : ModBuff
	{
		//public override string Texture => "CalamityAmmo/Misc/tideOfVictory2";
		public override void SetStaticDefaults()
		{
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			modplayer.victide = true;
		}
	}
}
