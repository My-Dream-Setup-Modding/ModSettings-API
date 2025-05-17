using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using BepInEx.Configuration;
using UI.MainMenu;

namespace MdsOverlayApi
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepinexLoader : BaseUnityPlugin
    {
        public const string
        MODNAME = "MdsOverlayAPI",
        AUTHOR = "Edsil",
        GUID = AUTHOR + "." + MODNAME,
        VERSION = "1.0.0";

        public static Harmony ModSettingsApiHarmony { get; } = new Harmony(MODNAME);
        public static ManualLogSource Log { get; set; }

        public static GameObject OverlayApi { get; private set; }

        public void Awake()
        {
            OverlayApi = this.gameObject;
            Log = Logger;
            //var devLog = Config.Bind<bool>("Development", "DebugMessages", false, "If this mod writes debug Messages, mainly used for PrintF debugging.");
            //var devView = Config.Bind<bool>("Development", "DebugView", false, "If this mod creates test views");

            //LogManager.DebugActive = devLog.Value;

            ////LogManager.DebugActive = Config.Bind<bool>("Development", "DebugMessages", false, "If this mod writes debug Messages, " +
            ////    "mainly used for PrintF Debugging.");

            //if (devView.Value)
            //{
            //    TestDataManager.Init();
            //    DebugModManager.Init();
            //}
        }
    }
}
