using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModSettingsApi.Models
{
    public interface SettingsModel<T>
    {
        T Value { get; set; }
        SettingsVariant Variant { get; }
    }
}
