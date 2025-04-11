using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using ModSettingsApi.Patches;
using ModSettingsApi.Manager;

namespace ModSettingsApi
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepinexLoader : BaseUnityPlugin
    {
        public const string
        MODNAME = "ModSettingsApi",
        AUTHOR = "Edsil",
        GUID = AUTHOR + "." + MODNAME,
        VERSION = "1.0.0";

        public static Harmony ModSettingsApiHarmony { get; } = new Harmony(MODNAME);
        public static ManualLogSource Log { get; set; }

        public void Awake()
        {
            Log = Logger;
            ModSettingsApiHarmony.PatchAll(typeof(MainMenuUiPatches));

            //LogManager.DebugActive = Config.Bind<bool>("Development", "DebugMessages", false, "If this mod writes debug Messages, " +
            //    "mainly used for PrintF Debugging.");

            TestDataManager.Init();
        }
    }
}
