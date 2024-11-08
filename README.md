# Dependencies

- [PreferenceSystem](https://steamcommunity.com/sharedfiles/filedetails/?id=2949018507)

# Adding custom designs

The mod loads json files from `%localappdata%low\It's Happening\PlateUp\UserData\KitchdenDesignManager\`

These json files can contain any number of designs and should follow this schema:

```
{
    "Design A": "[KitchenDesigner layout string]"
    "Design B": "[KitchenDesigner layout string]"
}
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

        Dictionary<string, string> GetDesigns()
        {
            return {
                {"Design A", "[KitchenDesigner layout string]"},
                {"Design B", "[KitchenDesigner layout string]"},
            };
        }
    }
}
```

Then register it with the `DesignLoader` once your mod has been activated.

```
DesignLoader.Register(new KitchdenDesignProvider());
```
