using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModSettingsApi.Models
{
    public class TabModel
    {
        public string ModName { get; set; }
        public List<ISingleSettingModel> Settings { get; set; }

        //Used alphabetically sorting Tabs.
        public override string ToString()
        {
            return ModName;
        }
    }
}
