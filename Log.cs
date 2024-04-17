using KitchenDesignManager;

static class Log
{
    public static void Info(string message)
    {
        Mod.Logger.LogInfo(message);
    }

    public static void Warning(string message)
    {
        Mod.Logger.LogWarning(message);
    }
}