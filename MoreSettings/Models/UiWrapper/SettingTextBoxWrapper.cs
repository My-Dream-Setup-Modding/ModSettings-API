using ModSettingsApi.Models.Ui;
using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.UiWrapper
{
    /// <summary>
    /// Wrapper to handle most ui interactions with the gameobjects.
    /// </summary>
    public class SettingTextBoxWrapper : BaseSetting<TextBoxVariant, SettingTextBoxWrapper>
    {
        private readonly TextMeshProUGUI _text;
        private readonly TextMeshProUGUI _greyText;
        private readonly TMP_InputField _textBox;

        public SettingTextBoxWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.transform.Find("Full Screen").GetComponent<TextMeshProUGUI>();
            _greyText = _managedGameObject.transform.Find("Text Area/Placeholder").GetComponent<TextMeshProUGUI>();
            _textBox = _managedGameObject.GetComponent<TMP_InputField>();
        }
        
        public static SettingTextBoxWrapper Create(SettingToggleButtonWrapper existingWrapper, GameObject existingTextBox)
        {

            var gmObj = GameObject.Instantiate(existingWrapper.ManagedGameObject, existingWrapper.ManagedGameObject.transform.parent);
            gmObj.name = $"Custom_ButtonSetting";

            //Removing copied toggle components from ToggleButtonWrapper.
            var rightChild = gmObj.transform.Find("FullScreenToggle");
            rightChild.gameObject.SetActive(false);
            UnityEngine.Object.Destroy(rightChild.gameObject);

            var textBox = GameObject.Instantiate(existingTextBox, gmObj.transform).GetComponent<RectTransform>();
            textBox.name = "CustomTextBox";
            //x = 250 is the value used by 
            textBox.anchoredPosition = new Vector2(250, 0);

            return new SettingTextBoxWrapper(gmObj);
        }

        public override SettingTextBoxWrapper Instatiate(Transform parent, TextBoxVariant settingModel)
        {
            var gmObj = GameObject.Instantiate(ManagedGameObject, parent);
            gmObj.name = $"{((IVariant)settingModel).ParentMod.ModName}_TextBox_{settingModel.SettingsText}";
            var textBox = new SettingTextBoxWrapper(gmObj);
            //Switching to the copied gameobject context.
            return textBox.Instantiate(settingModel);
        }

        private SettingTextBoxWrapper Instantiate(TextBoxVariant settingModel)
        {
            _text.SetText(settingModel.SettingsText);
            _greyText.SetText(settingModel.GreyText);
            _textBox.textComponent.SetText(settingModel.CurrentValue);

            _textBox.onValueChanged.RemoveAllListeners();
            _textBox.onValueChanged = new TMP_InputField.OnChangeEvent();
            _textBox.onValueChanged.AddListener(settingModel.TextHasChanged);

            return null;
        }
    }
}
