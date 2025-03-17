using ModSettingsApi.Manager;
using ModSettingsApi.Models;

namespace ModSettingsApi.Api
{
    /// <summary>
    /// An API to allow mods, to inject their own settings window easily into MDS.
    /// </summary>
    public static class ModSettingsApi
    {
        //public static void AddBase
        /// <summary>
        /// Adds <paramref name="modSettings"/> to the ModSettings menu.
        /// Settings will be sorted alphabetically, for a more constant experience.
        /// </summary>
        public static void AddMod(TabModel modSettings)
        {
            SettingsManager.AddModTab(modSettings);
        }
    }
}
