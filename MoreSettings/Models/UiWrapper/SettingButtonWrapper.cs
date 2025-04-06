using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingButtonWrapper : BaseSetting<ButtonVariant>
    {
        public SettingButtonWrapper(SettingSliderWrapper existingWrapper, GameObject existingButton)
        {

        }

        public override GameObject Instatiate(Transform parent, ButtonVariant settingModel)
        {
            throw new NotImplementedException();
        }
    }
}
