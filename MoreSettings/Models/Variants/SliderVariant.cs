using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;

namespace ModSettingsApi.Models.Variants
{
    public class SliderVariant : IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant => SettingsVariant.Slider;
        public object CurrentValue { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public SliderVariant(string settingsText)
        {
            SettingsText = settingsText;
        }
    }
}
