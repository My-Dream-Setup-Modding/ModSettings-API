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
        public SettingTextBoxWrapper(SettingSliderWrapper existingWrapper, GameObject existingTextBox)
        {
            _managedGameObject = new GameObject("Custom_ButtonSetting");
            
        }

        public override SettingTextBoxWrapper Instatiate(Transform parent, TextBoxVariant settingModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
