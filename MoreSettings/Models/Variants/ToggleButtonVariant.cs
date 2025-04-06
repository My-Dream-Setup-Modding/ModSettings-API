using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;
using UnityEngine.Events;
using static UnityEngine.UI.Toggle;

namespace ModSettingsApi.Models.Variants
{
    public class ToggleButtonVariant : IVariant
    {
        public string SettingsName { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }
        public ToggleEvent ValueChanged { get; } = new ToggleEvent();

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public ToggleButtonVariant(string settingsName, UnityAction<bool> valueChanged, bool defaultValue = false)
        {
            SettingsName = settingsName;
            ValueChanged.AddListener(valueChanged);
            CurrentValue = defaultValue;
        }
    }
}
