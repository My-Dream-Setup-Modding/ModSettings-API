using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using ModSettingsApi.Models;
using ModSettingsApi.Patches;

namespace ModSettingsApi
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepinexLoader : BaseUnityPlugin
    {
        public const string
        MODNAME = "ModSettingsApi",
        AUTHOR = "Endskill",
        GUID = AUTHOR + "." + MODNAME,
        VERSION = "1.0.0";

        public static Harmony ModSettingsApiHarmony { get; } = new Harmony(MODNAME);
        public static ManualLogSource Log { get; set; }

        public void Awake()
        {
            Log = Logger;
            ModSettingsApiHarmony.PatchAll(typeof(MainMenuUiPatches));
        }
    }
}
