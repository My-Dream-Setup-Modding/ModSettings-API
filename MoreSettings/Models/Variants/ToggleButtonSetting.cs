using ModSettingsApi.Models.Enums;

namespace ModSettingsApi.Models.Variants
{
    public class ToggleButtonSetting : ISingleSettingModel
    {
        public delegate void DelegateValueChanged(bool newValue);

        public string SettingsName { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }
        public DelegateValueChanged ValueChanged { get; }

        public ToggleButtonSetting(string settingsName, DelegateValueChanged valueChanged, bool defaultValue = false)
        {
            SettingsName = settingsName;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }
    }
}
