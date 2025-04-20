# ModSettings API

## About

ModSettings API allows other mods, to create a simple setting windows in the game "My Dream Setup".

## Features

ModSettings API can be used by creating a new TabModel instance, and giving it the API with: ModdedSettingsApi.AddMod(TabModel modSettings)

There are currently the following components available: 
ButtonSetting, TextBoxSetting, ComboBoxSetting, ToggleButtonSetting

These can be used by creating an instance of ButtonVariant, TextBoxVariant, ComboBoxVariant, ToggleButtonVariant, which require all data, to visualize the wanted setting, those variant wrappers are readonly and don't support changes mid game.

## Use case

The API call to add the settings page for any mod, should be done very early, since the api only creates the entire view the first time and does not react to changes from the Variant or TabModel instances anymore.

This mod was mainly to get familiar with the ui components in MDS, and such is most likely not that useful, a settings window can be created like this:

public static void Init()
{
    var lst = new List<ModSettingsApi.Models.Variants.IVariant>();
    lst.Add(new ModSettingsApi.Models.Variants.ToggleButtonVariant("DebugToggle", ToggleChangedDebug, true));
    var combo = new ModSettingsApi.Models.Variants.ComboBoxVariant("DebugCombo", ComboValueChanged, new List<IComboBoxData>()
    {
        new ModSettingsApi.Models.Variants.ComboBoxOptionData("Debug_1"),
        new ModSettingsApi.Models.Variants.ComboBoxOptionData("Debug_2"),
        new ModSettingsApi.Models.Variants.ComboBoxOptionData("Debug_3"),
        new ModSettingsApi.Models.Variants.ComboBoxOptionData("Debug_4")
    });
    lst.Add(combo);
    lst.Add(new ModSettingsApi.Models.Variants.ButtonVariant("ButtonDebugOption", "BtnText", ButtonPressed));
    //lst.Add(new SliderVariant("SliderOption"));
    lst.Add(new ModSettingsApi.Models.Variants.TextBoxVariant("TextBoxDebugOption", "GreyText", TextBoxHasChanged, "TextBoxDefaultValue"));
    var settingsData = new ModSettingsApi.Models.TabModel("DebugData Mod", lst);
    ModSettingsApi.Api.ModdedSettingsApi.AddMod(settingsData);
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
