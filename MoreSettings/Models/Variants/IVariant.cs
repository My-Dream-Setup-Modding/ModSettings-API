using ModSettingsApi.Models.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ModSettingsApi.Models.Variants
{
    public interface IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant { get; }

        /// <summary>
        /// Used internally to better handle settings to mod connections.
        /// </summary>
        [IgnoreDataMember]
        internal TabModel ParentMod { get; set; }
    }
}
