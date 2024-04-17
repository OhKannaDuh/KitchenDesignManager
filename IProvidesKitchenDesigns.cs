
using System.Collections.Generic;

namespace KitchenDesignManager
{
    interface IProvidesKitchenDesigns
    {
        public List<Entry> GetDesigns();
    }
}