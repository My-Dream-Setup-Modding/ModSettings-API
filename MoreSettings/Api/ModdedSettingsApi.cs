using ModSettingsApi.Manager;
using ModSettingsApi.Models;
using ModSettingsApi.Models.Variants;
using UnityEngine;

namespace ModSettingsApi.Api
{
    /// <summary>
    /// An API to allow mods, to inject their own settings window easily into MDS.
    /// </summary>
    public static class ModdedSettingsApi
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

    public static class RuntimeTabModelApi
    {
        public static void AddOnRuntime(this TabModel tab, IVariant customUiComponent)
        {
            tab.RuntimeChanges.Add(customUiComponent);
        }

        public static bool TryRemoveCustomComponentOnRuntime(this TabModel tab, GameObject customUiComponent)
        {
            return true;
        }

        
    }
}
