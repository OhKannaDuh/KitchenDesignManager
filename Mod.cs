using KitchenMods;
using System.Collections.Generic;
using System.Linq;
using KitchenDesignManager.Providers;
using PreferenceSystem;
using ONe.KitchenDesigner.KitchenDesigns;


namespace KitchenDesignManager
{

    public class Mod : IModInitializer
    {
        public const string MOD_GUID = "com.ohkannaduh.kdm";
        public const string MOD_NAME = "Kitchen Design Selector";
        public const string MOD_VERSION = "2.0.0";
        public const string MOD_AUTHOR = "OhKannaDuh, ThaMighty";
        public const string MOD_GAMEVERSION = ">=1.2.0";

        private static Dictionary<string, string> Designs = [];

        static PreferenceSystemManager PrefManager;
        public static void LoadDesigns()
        {
            foreach (IDesignProvider provider in DesignLoader.All())
            {
                foreach (KeyValuePair<string, string> kvp in provider.GetDesigns())
                    Designs.Add(kvp.Key, kvp.Value);
            }
        }

        public void PostActivate(KitchenMods.Mod mod)
        {
            DesignLoader.Register(new JsonDesignProvider());

            PrefManager = new PreferenceSystemManager(MOD_GUID, MOD_NAME);

            LoadDesigns();

            PrefManager
            .AddLabel("Kitchen Design Selector")
            .AddInfo("Select your favorite Kitchen Design")
            .AddOption<string>("designs", Designs.Keys.ToArray()[0], [.. Designs.Values], [.. Designs.Keys], true)
            .AddButton("Load Design", (Load) =>
            {
                KitchenDesignDecoder.TryDecode(PrefManager.Get<string>("designs"), out KitchenDesign design, out string message);
                KitchenDesignLoader.LoadKitchenDesign(design, "");
            });

            PrefManager.RegisterMenu(PreferenceSystemManager.MenuType.PauseMenu);
        }
        public void PreInject() { }
        public void PostInject() { }
    }
}

