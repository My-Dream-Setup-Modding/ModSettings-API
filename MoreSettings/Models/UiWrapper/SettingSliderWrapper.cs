using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using UI.CustomElements.CustomSliders;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    internal class SettingSliderWrapper : BaseSetting<SliderVariant, SettingSliderWrapper>
    {
        private readonly TextMeshProUGUI _text;
        private readonly SliderValueDisplay _sliderDisplay;

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
