using Terraria.ModLoader;

namespace CalamityAmmo.CAESystem
{
	public class CAEKeybinds : ModSystem
	{
		public static ModKeybind OddHotKey { get; private set; }
		public static ModKeybind autoCoilKey { get; private set; }
		public override void Load()
		{
			OddHotKey = KeybindLoader.RegisterKeybind(Mod, "Odd", Microsoft.Xna.Framework.Input.Keys.G);
			autoCoilKey = KeybindLoader.RegisterKeybind(Mod, "autoCoil", Microsoft.Xna.Framework.Input.Keys.F);
		}
		public override void Unload()
		{
			OddHotKey = null;
			autoCoilKey = null;
		}
	}
}
