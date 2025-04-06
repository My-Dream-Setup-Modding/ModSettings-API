using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingToggleButtonWrapper : BaseSetting<ToggleButtonVariant>
    {
        public SettingToggleButtonWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public override GameObject Instatiate(Transform parent, ToggleButtonVariant settingModel)
        {
            
        }
    }
}
