using BepInEx.Logging;
using BepInEx;
using HarmonyLib;
using MoreSettings.Models;

namespace MoreSettings
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepinexLoader : BaseUnityPlugin
    {
        public const string
        MODNAME = "MoreSettings",
        AUTHOR = "Endskill",
        GUID = AUTHOR + "." + MODNAME,
        VERSION = "1.0.0";

        public static Harmony ScaleExpanderHarmony { get; } = new Harmony(MODNAME);
        public static ManualLogSource Log { get; set; }

        public void Awake()
        {
            Log = Logger;
        }

    }
}
