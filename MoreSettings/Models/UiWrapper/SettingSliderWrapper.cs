using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingSliderWrapper : BaseSetting<SliderVariant, SettingSliderWrapper>
    {
        private readonly TextMeshProUGUI _text;

        public SettingSliderWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.transform.Find("Music").GetComponent<TextMeshProUGUI>();
        }

        public override SettingSliderWrapper Instatiate(Transform parent, SliderVariant settingModel)
        {
            return null;
        }
    }
}
