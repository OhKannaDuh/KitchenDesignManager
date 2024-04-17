# Dependencies

- [KitchenLib](https://steamcommunity.com/workshop/filedetails/?id=2898069883)
- [Kitchen Designer](https://steamcommunity.com/sharedfiles/filedetails/?id=2901012380)
- [HarmonyX](https://steamcommunity.com/workshop/filedetails/?id=2898033283)

# Adding custom designs

The mod loads json files from `%localappdata%low/It's Happening/PlateUp/UserData/KitchdenDesignManager\`

These json files can contain any number of designs and should follow this schema:

```
[
    {
        "label": "Design A",
        "design": "[KitchenDesigner layout string]"
    },
    {
        "label": "Design B",
        "design": "[KitchenDesigner layout string]"
    }
]
```

## Sharing custom designs on the workshop

You can create a mod to easily share custom designs and register them with this mod. To do so create a provider class that implements the `IProvidesKitchenDesigns` interface.

Example:

```
using KitchenDesignManager;

namespace MyMod
{
    class KitchdenDesignProvider : IProvidesKitchenDesigns
    {

        public List<Entry> GetDesigns()
        {
            return [
                new Entry("Design A", "[KitchenDesigner layout string]"),
                new Entry("Design B", "[KitchenDesigner layout string]"),
            ];
        }
    }
}
```

Then register it with the `DesignLoader` once your made has been activated.

```
DesignLoader.Register(new KitchdenDesignProvider());
```
