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
        }

        //Deactivate mod settings window, when any other game window gets open.
        //Close the modded settings view, when any other view gets open.
        [HarmonyPatch(typeof(MainMenuUI), nameof(MainMenuUI.HideNews))]
        [HarmonyPrefix]
        public static void HideNewsPrefix()
        {
            PanelUiManager.Instance?.ClosePanel();
        }
    }
}
