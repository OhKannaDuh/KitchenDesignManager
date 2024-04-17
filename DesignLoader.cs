
using System.Collections.Generic;

namespace KitchenDesignManager
{
    static class DesignLoader
    {
        private static List<IProvidesKitchenDesigns> Providers = new();

        public static void Register(IProvidesKitchenDesigns provider)
        {
            Providers.Add(provider);
        }

        public static List<IProvidesKitchenDesigns> All()
        {
            return Providers;
        }
    }
}