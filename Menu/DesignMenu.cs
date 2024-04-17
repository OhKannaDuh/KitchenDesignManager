using UnityEngine;
using Kitchen.Modules;
using Kitchen;
using ONe.KitchenDesigner.KitchenDesigns;
using System.Collections.Generic;

namespace KitchenDesignManager.Menu
{
    class DesignMenu : Menu<PauseMenuAction>
    {
        public DesignMenu(Transform container, ModuleList moduleList) : base(container, moduleList) { }

        public override void Setup(int playerId)
        {
            AddLabel("Kitchen Designs");

            List<string> names = new List<string>();
            foreach (Entry entry in Mod.Entries)
            {
                names.Add(entry.label);
            }

            Option<Entry> options = new Option<Entry>(
                Mod.Entries,
                Mod.Entries[0],
                names
            );

            SelectElement select = AddSelect<Entry>(options);
            select.OnOptionChosen += (int index) =>
            {
                Entry entry = Mod.Entries[index];
                Log.Info(index + " " + entry.label + " - " + entry.design);

                KitchenDesign design = null;
                string message = null;
                KitchenDesignDecoder.TryDecode(entry.design, out design, out message);
                if (message != "")
                {
                    Log.Info(message);
                }

                KitchenDesignLoader.LoadKitchenDesign(design, "");
                RequestAction(PauseMenuAction.CloseMenu);
            };

        }
    }
}