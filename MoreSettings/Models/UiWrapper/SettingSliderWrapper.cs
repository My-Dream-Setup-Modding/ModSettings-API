using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingSliderWrapper : BaseSetting<SliderVariant>
    {
        public SettingSliderWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.transform.Find("Music").GetComponent<TextMeshProUGUI>();
        }

        public override GameObject Instatiate(Transform parent, SliderVariant settingModel)
        {
            throw new NotImplementedException();
        }
    }
}
