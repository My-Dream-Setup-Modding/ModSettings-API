using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingButtonWrapper : BaseSetting<ButtonVariant, SettingButtonWrapper>
    {
        public SettingButtonWrapper(SettingSliderWrapper existingWrapper, GameObject existingButton)
        {
            var gmObj = GameObject.Instantiate(existingWrapper.ManagedGameObject, existingWrapper.ManagedGameObject.transform.parent);
            gmObj.name = $"Custom_ButtonSetting";


        }

        public override SettingButtonWrapper Instatiate(Transform parent, ButtonVariant settingModel)
        {


            return null;
        }
    }
}
