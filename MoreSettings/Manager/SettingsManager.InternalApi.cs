using ModSettingsApi.Models;
using System.Collections.Generic;

namespace ModSettingsApi.Manager
{
    internal partial class SettingsManager
    {
        public static List<TabModel> Tabs { get; set; } = new List<TabModel>();

        public static void AddModTab(TabModel mod)
        {
            Tabs.Add(mod);
            Tabs.Sort();

            LogManager.Debug("New tab got added to the modded settings.");

            if (_instance != null)
            {
                
                //rebuild existing settings tab.
            }
        }
    }
}
