using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;
using UnityEngine.Events;

namespace ModSettingsApi.Models.Variants
{
    public class ButtonVariant : IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public UnityAction ButtonClick { get; set; }
        public UnityAction<ButtonVariant> ButtonClick_SelfRef { get; set; }

        public ButtonVariant(string settingsName, UnityAction click)
        {
            SettingsText = settingsName;
            ButtonClick = click;
        }

        public ButtonVariant(string settingsName, UnityAction<ButtonVariant> click)
        {
            SettingsText = settingsName;
            ButtonClick_SelfRef = click;
        }

        public void InvokeSetting()
        {
            ButtonClick?.Invoke();
            ButtonClick_SelfRef?.Invoke(this);
        }
    }
}
