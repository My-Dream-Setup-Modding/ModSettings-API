using MoreSettings.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoreSettings.Models
{
    public interface SettingsModel<T>
    {
        T Value { get; set; }
        SettingsVariant Variant { get; }
    }
}
