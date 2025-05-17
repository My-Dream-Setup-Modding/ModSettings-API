using ModSettingsApi.Models.Enums;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ModSettingsApi.Models.Variants
{
    public interface IVariant
    {
        public SettingsVariant Variant { get; }

        /// <summary>
        /// Used internally to better handle settings to mod connections.
        /// </summary>
        [IgnoreDataMember]
        internal TabModel ParentMod { get; set; }
    }

    /// <summary>
    /// Used to have an external way, to 
    /// </summary>
    public abstract class AbstractIVariant : IVariant
    {
        public abstract SettingsVariant Variant { get; }
        TabModel IVariant.ParentMod { get; set; }
    }
}
