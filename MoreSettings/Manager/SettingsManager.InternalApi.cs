using ModSettingsApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ModSettingsApi.Manager
{
    internal partial class SettingsManager
    {
        public static List<TabModel> Tabs { get; set; } = new List<TabModel>();

        public static void AddModTab(TabModel mod)
        {
            Tabs.Add(mod);
            Tabs = Tabs.OrderBy(x => x.ModName).ToList();

            LogManager.Debug("New tab got added to the modded settings.");
        }
    }
}
