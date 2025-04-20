using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using ModSettingsApi.Patches;
using ModSettingsApi.Manager;
using UnityEngine;
using BepInEx.Configuration;

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

        public static GameObject ModSettingsApi { get; private set; }

        public void Awake()
        {
            ModSettingsApi = this.gameObject;
            Log = Logger;
            ModSettingsApiHarmony.PatchAll(typeof(MainMenuUiPatches));

            //LogManager.DebugActive = Config.Bind<bool>("Development", "DebugMessages", false, "If this mod writes debug Messages, " +
            //    "mainly used for PrintF Debugging.");

            TestDataManager.Init();
            DebugModManager.Init(Config);
        }
    }
}
