using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingToggleButtonWrapper : BaseSetting<ToggleButtonVariant, SettingToggleButtonWrapper>
    {
        public SettingToggleButtonWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public override SettingToggleButtonWrapper Instatiate(Transform parent, ToggleButtonVariant settingModel)
        {
            return null;
        }
    }
}
