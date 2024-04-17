
using Kitchen.Modules;
using Kitchen;
using HarmonyLib;
using System.Reflection;
using KitchenDesignManager.Menu;


[HarmonyPatch(typeof(MainMenu), "Setup")]
class MainMenuEntry
{
    private static bool Prefix(MainMenu __instance)
    {
        MethodInfo addSubmenu = __instance.GetType().GetMethod("AddSubmenuButton", BindingFlags.NonPublic | BindingFlags.Instance);
        addSubmenu.Invoke(__instance, new object[] { KitchenDesignManager.Mod.MOD_NAME, typeof(DesignMenu), false });
        return true;
    }
}

[HarmonyPatch(typeof(PlayerPauseView), "SetupMenus")]
class PauseMenuEntry
{
    private static bool Prefix(PlayerPauseView __instance)
    {
        ModuleList moduleList = (ModuleList)__instance.GetType().GetField("ModuleList", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
        MethodInfo addMenu = __instance.GetType().GetMethod("AddMenu", BindingFlags.NonPublic | BindingFlags.Instance);
        addMenu.Invoke(__instance, new object[] { typeof(DesignMenu), new DesignMenu(__instance.ButtonContainer, moduleList) });
        return true;
    }
}