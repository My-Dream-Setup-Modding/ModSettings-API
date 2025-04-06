using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.Ui
{
    public class SettingComboBoxWrapper : BaseSetting<ComboBoxVariant>
    {
        public SettingComboBoxWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.transform.Find("Resolution").GetComponent<TextMeshProUGUI>();
        }

        public override GameObject Instatiate(Transform parent, ComboBoxVariant settingModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
