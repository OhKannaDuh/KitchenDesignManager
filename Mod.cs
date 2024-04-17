using KitchenLib;
using KitchenLib.Logging;
using KitchenMods;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;
using KitchenDesignManager.Providers;


namespace KitchenDesignManager
{
    public class Mod : BaseMod, IModSystem
    {
        public const string MOD_GUID = "com.ohkannaduh.kdm";
        public const string MOD_NAME = "Kitchen Design Manager";
        public const string MOD_VERSION = "1.0.0";
        public const string MOD_AUTHOR = "OhKannaDuh";
        public const string MOD_GAMEVERSION = ">=1.1.4";

        public static List<Entry> Entries = new List<Entry>();

        public static KitchenLogger Logger;

        public Mod() : base(MOD_GUID, MOD_NAME, MOD_AUTHOR, MOD_VERSION, MOD_GAMEVERSION, Assembly.GetExecutingAssembly())
        {
        }

        protected override void OnInitialise()
        {
            Logger.LogInfo($"{MOD_GUID} v{MOD_VERSION} initialising...");

            // Load from providers
            foreach (IProvidesKitchenDesigns provider in DesignLoader.All())
            {
                Entries = Entries.Concat(provider.GetDesigns()).ToList();
            }
        }

        protected override void OnPostActivate(KitchenMods.Mod mod)
        {
            Logger = InitLogger();

            DesignLoader.Register(new JsonDesignProvider());
        }
    }
}

