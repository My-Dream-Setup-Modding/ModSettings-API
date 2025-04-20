using ModSettingsApi.Models.Enums;
using System.Runtime.Serialization;

namespace ModSettingsApi.Models.Variants
{
    internal class ListNavigatorVariant : IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant => SettingsVariant.ListNavigator;
        public bool CurrentValue { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }
    }
}
