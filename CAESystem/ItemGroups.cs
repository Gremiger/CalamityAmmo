using CalamityAmmo.Accessories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Map;
using Terraria.ModLoader;

namespace CalamityAmmo.CAESystem
{
	public class ItemGroups:ModSystem
	{
		public static List<int> coils = new List<int>();
		public override void PostSetupContent()
		{
			coils = new List<int>()
			{
				ModContent.ItemType<ModifiedCoil>(),
				ModContent.ItemType<WulfrumCoil>(),
				//ModContent.ItemType<TitaniumCoil>(),
				//ModContent.ItemType<SuperCoil>(),
				ModContent.ItemType<FrigidCoil>(),
				ModContent.ItemType<AutoCalculationCoil>(),
				ModContent.ItemType<ScorchingCoil>(),
				ModContent.ItemType<TransformerCoil>()
			};
		}
	}
}
