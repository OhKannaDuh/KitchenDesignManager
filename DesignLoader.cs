using System.Collections.Generic;
using KitchenDesignManager.Providers;

namespace KitchenDesignManager
{
    static class DesignLoader
    {
        private static List<IDesignProvider> Providers = new();

        public static void Register(IDesignProvider provider)
        {
            Providers.Add(provider);
        }

        public static List<IDesignProvider> All()
        {
            return Providers;
        }
    }
}