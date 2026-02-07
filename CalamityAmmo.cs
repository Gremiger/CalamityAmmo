using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;



// 命名空间，注意它要与文件夹的名字相同
namespace CalamityAmmo
{
    // 主要Mod类
    public class CalamityAmmo : Mod
    {

        public static bool ShowDustTestUI = false;
        public static CalamityAmmo Instance;
        public static Effect exEffect;

        public override void PostSetupContent()
        {
            //如果有shader文件（后缀.xnb），那么请在这里保存，例： exEffect = Assets.Request<Effect>("路径/shader文件名（不带后缀）").Value;
        }

        internal static Dictionary<string, int> MyGlows;
        private List<Asset<Texture2D>> assets;
        public override void Load()
        {
            //这一部分代码是储存glowmask的基本代码，glowmask即覆盖在物品、盔甲等贴图上的一层发光贴图，原本需要手动导入，这段代码会帮助你自动导入
            //你储存在模组文件夹里的文件，只要文件名最后五个字是_Glow（不带后缀），就会被存入
            {
                MyGlows = new Dictionary<string, int>();
                assets = new List<Asset<Texture2D>>();
                string assetsPath;
                int i = 0;

                foreach (Type type in typeof(CalamityAmmo).Assembly.GetTypes().OrderBy((Type t) => t.FullName, StringComparer.InvariantCulture))
                {
                    assetsPath = type.FullName.Replace('.', '/') + "_Glow";
                    if (!type.IsAbstract && type.IsSubclassOf(typeof(ModItem)) && ModContent.HasAsset(assetsPath))
                    {
                        i++;
                        assets.Add(ModContent.Request<Texture2D>(assetsPath));
                        MyGlows.Add(type.Name, TextureAssets.GlowMask.Length - 1 + i);
                    }
                }

                List<Asset<Texture2D>> assetsGlow = new List<Asset<Texture2D>>(TextureAssets.GlowMask);
                assetsGlow.AddRange(assets);
                TextureAssets.GlowMask = assetsGlow.ToArray();
            }

        }

        public override void Unload()
        {
            //这三行在Mod卸载时会把存入的glowmask也卸掉，让出内存
            //List<Asset<Texture2D>> assetsGlow = new List<Asset<Texture2D>>(TextureAssets.GlowMask);
            //assetsGlow.RemoveAll(x => assets.Find(y => y == x) != default(Asset<Texture2D>));
            //TextureAssets.GlowMask = assetsGlow.ToArray();
        }

        public CalamityAmmo()
        {
            Instance = this;
            // MOD的初始化函数，用来设置一些属性
            // 注意，这跟Load() 函数不一样

            // 以上这些了解一下就行了，每个mod都要有这个
        }
        



        }
}
