using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;
using UnityEngine.Events;

namespace ModSettingsApi.Models.Variants
{
    public class ButtonVariant : IVariant
    {
        public string SettingsText { get; }
        public string ButtonText { get; }
        public SettingsVariant Variant => SettingsVariant.Button;

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public UnityAction ButtonClick { get; set; }
        public UnityAction<ButtonVariant> ButtonClick_SelfRef { get; set; }

        public ButtonVariant(string settingsName, string buttonText, UnityAction click)
        {
            SettingsText = settingsName;
            ButtonText = buttonText;
            ButtonClick = click;
        }

        public ButtonVariant(string settingsName, string buttonText, UnityAction<ButtonVariant> click)
        {
            SettingsText = settingsName;
            ButtonText = buttonText;
            ButtonClick_SelfRef = click;
        }

        public void InvokeSetting()
        {
            ButtonClick?.Invoke();
            ButtonClick_SelfRef?.Invoke(this);
        }
    }
}
