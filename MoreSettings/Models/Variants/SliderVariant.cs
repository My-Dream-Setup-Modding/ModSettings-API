using ModSettingsApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using static ModSettingsApi.Models.Variants.ButtonVariant;
using static UnityEngine.UI.Toggle;

namespace ModSettingsApi.Models.Variants
{
    public class SliderVariant : IVariant
    {
        public string SettingsName { get; }
        public SettingsVariant Variant => SettingsVariant.ToggleButton;
        public bool CurrentValue { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }
    }
}
