using ModSettingsApi.Api;
using ModSettingsApi.Models;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModSettingsApi.Manager
{
    public static class TestDataManager
    {
        /// <summary>
        /// Fakes all interaction a normal mod, would do for debugging reasons.
        /// </summary>
        public static void Init()
        {
            var lst = new List<IVariant>();
            lst.Add(new ToggleButtonVariant("TestToggle", ToggleChangedTest, true));
            var settingsData = new TabModel("TestData Mod", lst);

            ModdedSettingsApi.AddMod(settingsData);
        }

        private static void ToggleChangedTest(bool newValue)
        {
            LogManager.Message($"TestDataManager: Toggle changed, new Value: {newValue}");
        }
    }
}
