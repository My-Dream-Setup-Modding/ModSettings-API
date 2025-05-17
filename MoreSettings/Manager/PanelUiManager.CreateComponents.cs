using ModSettingsApi.Models;
using ModSettingsApi.Models.Enums;
using ModSettingsApi.Models.Variants;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Manager
{
    public partial class PanelUiManager
    {
        public GameObject CreateModView(VerticalLayoutGroup existingView, string name)
        {
            var parent = existingView.transform.parent;

            var view = GameObject.Instantiate(existingView, parent, true);
            view.name = name;
            view.transform.localScale = Vector3.one;

            return view.gameObject;
        }

        internal void UpdateView(TabModel tabModel)
        {
            LogManager.Message($"Updating settings view, for {tabModel.ModName}");

        }

        private void BuildView()
        {
            LogManager.Message("Start building modded views.");

            foreach (var pair in Views)
            {
                TabModel mod = pair.Key;
                GameObject ui = pair.Value;
                var debugCompList = new List<object>();
                DebugComponentList.Add(mod, debugCompList);

                LogManager.Message($"Building view for {mod.ModName}");

                foreach (var iSetting in mod.Settings)
                {
                    var newObject = InstantiateVariant(ui.transform, iSetting);
                    debugCompList.Add(newObject);
                }
            }
        }

        private object InstantiateVariant(Transform parent, IVariant iSetting)
        {
            switch (iSetting.Variant)
            {
                case SettingsVariant.Button:
                    var button = _uiButton.Instatiate(parent, (ButtonVariant)iSetting);
                    return button;
                case SettingsVariant.ToggleButton:
                    var toggle = _uiToggleButton.Instatiate(parent, (ToggleButtonVariant)iSetting);
                    return toggle;
                case SettingsVariant.Slider:
                    var slider = _uiSlider.Instatiate(parent, (SliderVariant)iSetting);
                    return slider;
                case SettingsVariant.ComboBox:
                    var combo = _uiComboBox.Instatiate(parent, (ComboBoxVariant)iSetting);
                    return combo;
                //case SettingsVariant.ListNavigator:
                //    LogManager.Warn($"ListNavigator not implemented yet.");
                //    break;
                case SettingsVariant.TextInput:
                    var textBox = _uiTextBox.Instatiate(parent, (TextBoxVariant)iSetting);
                    return textBox;
                case SettingsVariant.Custom:
                    var customOverlay = ((CustomVariant)iSetting).Instantiate(parent);
                    return customOverlay;
            }

            throw new KeyNotFoundException();
        }
    }
}
