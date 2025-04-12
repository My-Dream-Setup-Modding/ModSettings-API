using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;
using UnityEngine.Events;
using static UnityEngine.UI.Toggle;

namespace ModSettingsApi.Models.Variants
{
    public class ToggleButtonVariant : IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }
        public UnityAction<bool> ValueChanged { get; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public ToggleButtonVariant(string settingsName, UnityAction<bool> valueChanged, bool defaultValue = false)
        {
            SettingsText = settingsName;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }

        internal void OnValueHasChanged(bool newValue)
        {
            if (CurrentValue == newValue)
                return;

            CurrentValue = newValue;
            ValueChanged?.Invoke(newValue);
        }
    }
}
