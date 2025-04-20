using BepInEx.Configuration;
using ModSettingsApi.Api;
using ModSettingsApi.Models;
using ModSettingsApi.Models.Variants;
using System.Collections.Generic;

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
            var test = new ComboBoxVariant("TestCombo", ComboValueChanged, new List<IComboBoxData>()
            {
                new ComboBoxOptionData("Option_1"),
                new ComboBoxOptionData("Option_2"),
                new ComboBoxOptionData("Option_3"),
                new ComboBoxOptionData("Option_4")
            });
            lst.Add(test);
            lst.Add(new ButtonVariant("ButtonOption", "BtnText", ButtonPressed));
            //lst.Add(new SliderVariant("SliderOption"));
            lst.Add(new TextBoxVariant("TextBoxOption", "GreyText", TextBoxHasChanged, "TextBoxDefaultValue"));
            var settingsData = new TabModel("TestData Mod", lst);
            

            ModdedSettingsApi.AddMod(settingsData);
        }

        public static void ButtonPressed(ButtonVariant sender)
        {
            LogManager.Message($"TestDataManager: Button pressed, sender: {sender.SettingsText}");
        }

        private static void ToggleChangedTest(bool newValue)
        {
            LogManager.Message($"TestDataManager: Toggle changed, new Value: {newValue}");
        }

        private static void ComboValueChanged(IComboBoxData data)
        {
            LogManager.Message($"TestDataManager: Combo value changed, new Value: {data.Text}");
        }

        private static void TextBoxHasChanged(string newValue)
        {
            LogManager.Message($"TestDataManager: TextBox value changed, new Value: {newValue}");
        }
    }

    public static class DebugModManager
    {
        /// <summary>
        /// Fakes all interaction a normal mod, would do for debugging reasons.
        /// </summary>
        public static void Init()
        {
            var lst = new List<IVariant>();
            lst.Add(new ToggleButtonVariant("DebugToggle", ToggleChangedDebug, true));
            var Debug = new ComboBoxVariant("DebugCombo", ComboValueChanged, new List<IComboBoxData>()
            {
                new ComboBoxOptionData("Debug_1"),
                new ComboBoxOptionData("Debug_2"),
                new ComboBoxOptionData("Debug_3"),
                new ComboBoxOptionData("Debug_4")
            });
            lst.Add(Debug);
            lst.Add(new ButtonVariant("ButtonDebugOption", "BtnText", ButtonPressed));
            //lst.Add(new SliderVariant("SliderOption"));
            lst.Add(new TextBoxVariant("TextBoxDebugOption", "GreyText", TextBoxHasChanged, "TextBoxDefaultValue"));
            var settingsData = new TabModel("DebugData Mod", lst);

            ModdedSettingsApi.AddMod(settingsData);
        }

        public static void ButtonPressed(ButtonVariant sender)
        {
            LogManager.Message($"DebugDataManager: Button pressed, sender: {sender.SettingsText}");
        }

        private static void ToggleChangedDebug(bool newValue)
        {
            LogManager.Message($"DebugDataManager: Toggle changed, new Value: {newValue}");
        }

        private static void ComboValueChanged(IComboBoxData data)
        {
            LogManager.Message($"DebugDataManager: Combo value changed, new Value: {data.Text}");
        }

        private static void TextBoxHasChanged(string newValue)
        {
            LogManager.Message($"DebugDataManager: TextBox value changed, new Value: {newValue}");
        }

        public enum TestEnum
        {
            en1,
            en2,
            en3
        }
    }
}
