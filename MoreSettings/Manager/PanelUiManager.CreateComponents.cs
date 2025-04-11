using ModSettingsApi.Models;
using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
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
                    switch (iSetting.Variant)
                    {
                        case Models.Enums.SettingsVariant.Button:
                            LogManager.Warn($"Button not implemented yet.");
                            break;
                            var button = _uiButton.Instatiate(ui.transform, (ButtonVariant)iSetting);
                            debugCompList.Add(button);
                            break;
                        case Models.Enums.SettingsVariant.ToggleButton:
                            LogManager.Warn($"ToggleButton not implemented yet.");
                            break;
                            var toggle = _uiToggleButton.Instatiate(ui.transform, (ToggleButtonVariant)iSetting);
                            debugCompList.Add(toggle);
                            break;
                        case Models.Enums.SettingsVariant.Slider:
                            LogManager.Warn($"Slider not implemented yet.");
                            break;
                            var slider = _uiSlider.Instatiate(ui.transform, (SliderVariant)iSetting);
                            debugCompList.Add(slider);
                            break;
                        case Models.Enums.SettingsVariant.ComboBox:
                            var combo = _uiComboBox.Instatiate(ui.transform, (ComboBoxVariant)iSetting);
                            debugCompList.Add(combo);
                            break;
                        case Models.Enums.SettingsVariant.ListNavigator:
                            LogManager.Warn($"ListNavigator not implemented yet.");
                            break;
                    }
                }
            }
        }
    }
}
