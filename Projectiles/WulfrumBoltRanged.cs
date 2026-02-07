using CalamityMod.Projectiles.Magic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
    public class WulfrumBoltRanged : WulfrumBolt
    {
        public override string Texture => "CalamityMod/Projectiles/InvisibleProj";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Wulfrum Bolt");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "钨钢光束");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "энергетический луч вульфрума");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Ranged;
        }
    }
}
