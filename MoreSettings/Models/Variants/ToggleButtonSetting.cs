using ModSettingsApi.Models.Enums;
using UnityEngine.Events;

namespace ModSettingsApi.Models.Variants
{
    public class ToggleButtonSetting : ISingleSettingModel
    {
        public delegate void DelegateValueChanged(bool newValue);

        public string SettingsName { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }
        public UnityAction ValueChanged { get; }

        public ToggleButtonSetting(string settingsName, UnityAction valueChanged, bool defaultValue = false)
        {
            SettingsName = settingsName;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }



    }
}
