using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;
using UnityEngine.Events;

namespace ModSettingsApi.Models.Variants
{
    public class TextBoxVariant : IVariant
    {
        public string SettingsText { get; }
        public string GreyText { get; }
        public SettingsVariant Variant => SettingsVariant.TextInput;
        public string CurrentValue { get; set; }

        public UnityAction<string> ValueChanged { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public TextBoxVariant(string settingsText, string greyText, UnityAction<string> valueChanged, string defaultValue)
        {
            SettingsText = settingsText;
            GreyText = greyText;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }

        internal void TextHasChanged(string newValue)
        {
            if(CurrentValue == newValue)
                return;

            CurrentValue = newValue;
            ValueChanged?.Invoke(newValue);   
        }
    }
}
