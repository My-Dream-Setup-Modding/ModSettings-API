using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ModSettingsApi
{
    internal static class LogManager
    {
        private static readonly ManualLogSource logger;

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
            logger.LogMessage(msg);
        }

        public static void Message(object msg)
        {
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
