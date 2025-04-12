using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Models.Ui
{
    public class SettingToggleButtonWrapper : BaseSetting<ToggleButtonVariant, SettingToggleButtonWrapper>
    {
        private readonly TextMeshProUGUI _text;
        private readonly Toggle _toggle;

        public SettingToggleButtonWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.GetComponentInChildren<TextMeshProUGUI>();
            _toggle = _managedGameObject.transform.Find("FullScreenToggle").GetComponent<Toggle>();
        }

        /// <summary>
        /// Get the managed toggle ui gameobject.
        /// </summary>
        internal Toggle Toggle => _toggle;

        public override SettingToggleButtonWrapper Instatiate(Transform parent, ToggleButtonVariant settingModel)
        {
            var gmObj = GameObject.Instantiate(ManagedGameObject, parent);
            gmObj.name = $"{((IVariant)settingModel).ParentMod.ModName}_ToggleButton_{settingModel.SettingsText}";
            var toggle = new SettingToggleButtonWrapper(gmObj);
            //Switching to the copied gameobject context.
            return toggle.Instantiate(settingModel);
        }

        private SettingToggleButtonWrapper Instantiate(ToggleButtonVariant settingModel)
        {
            _text.SetText(settingModel.SettingsText);

            _toggle.onValueChanged.RemoveAllListeners();
            _toggle.isOn = settingModel.CurrentValue;
            _toggle.onValueChanged = new Toggle.ToggleEvent();
            _toggle.onValueChanged.AddListener(settingModel.OnValueHasChanged);

            return this;
        }
    }
}
