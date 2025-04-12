using ModSettingsApi.Models.Variants;
using TMPro;
using UnityEngine;

namespace ModSettingsApi.Models.UiWrapper
{
    public abstract class BaseSetting<T, ParentSetting> : IBaseSetting 
        where T : IVariant where ParentSetting : IBaseSetting
    {
        protected GameObject _managedGameObject;
        public GameObject ManagedGameObject { get => _managedGameObject; }

        public abstract ParentSetting Instatiate(Transform parent, T settingModel);
    }

    public interface IBaseSetting
    {
        GameObject ManagedGameObject { get; }
    }
}
