using ModSettingsApi.Models.Enums;
using System.Collections.Generic;

namespace ModSettingsApi.Models.Variants
{
    public interface ISingleSettingModel
    {
        public string SettingsName { get; }
        public SettingsVariant Variant { get; }
    }
}
