using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.UiWrapper
{
    public abstract class BaseSetting<T> where T : IVariant
    {
        protected TextMeshProUGUI _text;
        protected GameObject _managedGameObject;
        public GameObject ManagedGameObject { get => _managedGameObject; }

        public void SetSettingText(string text)
        {
            _text.SetText(text);
        }

        public abstract GameObject Instatiate(Transform parent, T settingModel);
    }
}
