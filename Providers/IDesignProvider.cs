using System.Collections.Generic;

namespace KitchenDesignManager.Providers
{
    interface IDesignProvider
    {
        public Dictionary<string, string> GetDesigns();
    }
}