using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.UiWrapper
{
    public abstract class BaseSetting<T, ParentSetting> : IBaseSetting 
        where T : IVariant where ParentSetting : IBaseSetting
    {
        protected TextMeshProUGUI _text;
        protected GameObject _managedGameObject;
        public GameObject ManagedGameObject { get => _managedGameObject; }

        public void SetSettingText(string text)
        {
            _text.SetText(text);
        }

        public abstract ParentSetting Instatiate(Transform parent, T settingModel);
    }

    public interface IBaseSetting
    {
        GameObject ManagedGameObject { get; }
        void SetSettingText(string text);
    }
}
