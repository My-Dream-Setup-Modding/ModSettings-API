using HarmonyLib;
using ModSettingsApi.Manager;
using UI.MainMenu;

namespace ModSettingsApi.Patches
{
    [HarmonyPatch(typeof(MainMenuUI))]
    public static class MainMenuUiPatches
    {
        [HarmonyPatch(nameof(MainMenuUI.Awake))]
        [HarmonyPostfix]
        public static void AwakePostfix(MainMenuUI __instance)
        {
            LogManager.CategoryMessage();
            SettingsManager.Init(__instance);

            //ModSettingsApiManager.
        }
    }
}
