using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Models.Ui
{
    public class SettingButtonWrapper : BaseSetting<ButtonVariant, SettingButtonWrapper>
    {
        private readonly TextMeshProUGUI _text;
        private readonly TextMeshProUGUI _buttonText;
        private readonly Button _button;

        public SettingButtonWrapper(GameObject managedGameobject)
        {
            _managedGameObject = managedGameobject;
            _button = _managedGameObject.transform.Find("CustomButton").GetComponent<Button>();
            _buttonText = _button.GetComponentInChildren<TextMeshProUGUI>();
            _text = _managedGameObject.transform.Find("Full Screen").GetComponent<TextMeshProUGUI>();
        }

        public static SettingButtonWrapper Create(SettingToggleButtonWrapper existingWrapper, GameObject existingButton)
        {
            var gmObj = GameObject.Instantiate(existingWrapper.ManagedGameObject, existingWrapper.ManagedGameObject.transform.parent);
            gmObj.SetActive(true);
            gmObj.name = $"Custom_ButtonSetting";

            //Removing copied toggle components from ToggleButtonWrapper.
            var rightChild = gmObj.transform.Find("FullScreenToggle");
            rightChild.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(rightChild.gameObject);

            var btn = GameObject.Instantiate(existingButton, gmObj.transform).GetComponent<RectTransform>();
            btn.name = "CustomButton";
            //x = 250 is the value used by 
            btn.anchoredPosition = new Vector2(250, 0);

            return new SettingButtonWrapper(gmObj);
        }

        public override SettingButtonWrapper Instatiate(Transform parent, ButtonVariant settingModel)
        {
            var gmObj = GameObject.Instantiate(ManagedGameObject, parent);
            gmObj.gameObject.SetActive(true);
            gmObj.name = $"{((IVariant)settingModel).ParentMod.ModName}_Button_{settingModel.SettingsText}";
            var combo = new SettingButtonWrapper(gmObj);
            //Switching to the copied gameobject context.
            return combo.Instantiate(settingModel);
        }

        private SettingButtonWrapper Instantiate(ButtonVariant settingModel)
        {
            _text.SetText(settingModel.SettingsText);
            _buttonText.SetText(settingModel.ButtonText);
            _button.onClick.RemoveAllListeners();
            _button.onClick = new Button.ButtonClickedEvent();
            _button.onClick.AddListener(settingModel.InvokeSetting);

            return this;
        }
    }
}
