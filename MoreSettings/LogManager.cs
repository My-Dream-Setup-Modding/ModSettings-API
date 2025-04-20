using BepInEx.Logging;
using System.Runtime.CompilerServices;

namespace ModSettingsApi
{
    internal static class LogManager
    {
        private static readonly ManualLogSource logger;
        static internal bool DebugActive { get; set; }

        static LogManager()
        {
            logger = BepinexLoader.Log;
        }

        public static void Verbose(object msg)
        {
            logger.LogInfo(msg);
        }

        public static void Debug(object msg)
        {
            if (DebugActive)
                logger.LogMessage(msg);
        }

        public static void Message(object msg)
        {
            if (DebugActive)
                logger.LogMessage(msg);
        }

        public static void Error(object msg)
        {
            logger.LogError(msg);
        }

        public static void Warn(object msg)
        {
            logger.LogWarning(msg);
        }

        internal static void CategoryMessage([CallerMemberName] string category = "unknown???")
        {
            logger.LogMessage($"=======================================================================");
            logger.LogMessage(category);
            logger.LogMessage($"=======================================================================");
        }
    }
}
