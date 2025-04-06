using ModSettingsApi.Models.Enums;
using static UnityEngine.UI.Toggle;
using System.Runtime.Serialization;
using UnityEngine.Events;

namespace ModSettingsApi.Models.Variants
{
    public class ButtonVariant : IVariant
    {
        public string SettingsName { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public ButtonVariant(string settingsName, UnityAction click)
        {
            SettingsName = settingsName;
            //Click = click;
        }
    }
}
