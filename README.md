# Breaking Changes With Mod Version 2.0.0

The Layout of the JSON file has changed so to make the mod work again, if you were previously
on an older version, you have to either alter or delete your files at
`%localappdata%low\It's Happening\PlateUp\UserData\KitchdenDesignManager\`

Change the JSON Layout from
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

to

```
{
    "Design A": "[KitchenDesigner layout string]",
    "Design B": "[KitchenDesigner layout string]"
}
```

# Dependencies

- [PreferenceSystem](https://steamcommunity.com/sharedfiles/filedetails/?id=2949018507)

# Adding custom designs

The mod loads json files from `%localappdata%low\It's Happening\PlateUp\UserData\KitchdenDesignManager\`

These json files can contain any number of designs and should follow this schema:

```
{
    "Design A": "[KitchenDesigner layout string]",
    "Design B": "[KitchenDesigner layout string]"
}
```

# For Mod Authors
## Sharing custom designs on the workshop

You can create a mod to easily share custom designs and register them with this mod. To do so create a provider class that implements the `IDesignProvider` interface.

Example:

```
using KitchenDesignManager;

namespace MyMod
{
    class KitchdenDesignProvider : IDesignProvider
    {

        Dictionary<string, string> GetDesigns()
        {
            return {
                {
                "Design A", "[KitchenDesigner layout string]",
                "Design B", "[KitchenDesigner layout string]"
                 }
            };
        }
    }
}
```

Then register it with the `DesignLoader` once your mod has been activated.

```
DesignLoader.Register(new KitchdenDesignProvider());
```
