using BepInEx.Configuration;
using ModSettingsApi.Models;

namespace ModSettingsApi.Api
{
    public static class SettingsApiExtensions
    {
        public static TabModel AddToggleConfig(this TabModel tab, ConfigEntry<bool> config)
        {

            return tab;
        }

        public static TabModel AddTextConfig(this TabModel tab, ConfigEntry<string> config)
        {

            return tab;
        }
    }
}
