using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Text;
using UI.CustomElements.Tabs.SettingsTabs;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using ModSettingsApi.Models;
using TMPro;
using Unity.VisualScripting;
using ModSettingsApi.Models.Variants;
using UnityEngine.UIElements;
using Unity.VideoHelper;

namespace ModSettingsApi.Manager
{
    /// <summary>
    /// Manager to handle the modded ui panel.
    /// </summary>
    public class PanelUiManager
    {
        private readonly GameObject _uiButton;
        private readonly GameObject _uiToggleButton;
        private readonly GameObject _uiListNavigation;
        private readonly GameObject _uiSlider;

        private readonly GameObject _tabSplitter;
        private readonly GameObject _tabButton;

        private readonly MainMenuUI _ui;

        private RectTransform _panel;
        private HorizontalLayoutGroup _tabs;
        private SettingsTab _view;

        public Dictionary<TabModel, VerticalLayoutGroup> Views { get; set; } = new Dictionary<TabModel, VerticalLayoutGroup>();

        public PanelUiManager(MainMenuUI gameUi, List<TabModel> modsToRender)
        {
            Instance = this;

            _panel = _ui.settingsPanel.gameObject.Instantiate<RectTransform>("ModSettingsPanel");
            //Removing all TabButtons on the top, but ModSettings.
            _tabs = _panel.GetComponentInChildren<HorizontalLayoutGroup>();
            foreach (RectTransform child in _tabs.GetComponentInChildren<RectTransform>())
            {
                switch (child.name)
                {
                    case "Splitter":
                        if (_tabSplitter != null)
                            _tabSplitter = child.gameObject.Instantiate<RectTransform>("TabSplitter").gameObject;

                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                    case "General":
                        _tabButton = child.gameObject.Instantiate<RectTransform>("ModdedTab").gameObject;
                        child.name = "ModSettings API";
                        child.GetComponent<TextMeshProUGUI>().SetText("Mod Settings");
                        break;
                    default:
                        UnityEngine.Object.Destroy(child.gameObject);
                        break;

                }

            }

            _view = _panel.GetComponentInChildren<SettingsTab>();
            foreach (RectTransform child in _view.GetComponentInChildren<RectTransform>())
            {
                switch (child.name)
                {
                    case "General":
                        child.name = "ModdedView";
                        //Destroy the current inner Settings.
                        UnityEngine.Object.Destroy(child.GetComponent<VerticalLayoutGroup>());

                        foreach (var mod in modsToRender)
                        {
                            var group = child.AddComponent<VerticalLayoutGroup>();
                            Views.Add(mod, group);
                        }

                        break;

                    case "Graphics":
                        //Extracting ToggleButton.
                        //Extracting ListNavigation

                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                    case "Audio":
                        //Extracting UI Slider

                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                    default:
                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                }
            }

            BuildModdedViews();
        }

        public static PanelUiManager Instance { get; set; }

        public void OpenPanel()
        {
            _ui.CloseRoomSelection();
            _ui.CloseWorkshop();
            _ui.CloseCredits();
            _ui.HideNews();
            _ui.CloseSettings();
            _panel.transform.DOKill();
            _panel.transform.DOScale(1.1f, 0.25f).
                OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(
                () => _panel.transform.DOScale(1f, 0.1f));
        }

        public void ClosePanel()
        {
            _ui.ShowNews();
            _panel.transform.DOKill();
            _panel.transform.DOScale(0.0f, 0.25f);
        }

        private void BuildModdedViews()
        {
            foreach (var view in Views)
            {
                var mod = view.Key;
                var ui = view.Value;

                foreach (var iSetting in mod.Settings)
                {
                    switch (iSetting.Variant)
                    {
                        //    case Models.Enums.SettingsVariant.Button:
                        //        var setting = (ButtonSetting)iSetting;

                        //        var button = _uiButton.Instantiate<RectTransform>($"{mod.ModName}_{setting.SettingsName}");
                        //        break;
                        case Models.Enums.SettingsVariant.ToggleButton:
                            var setting = (ToggleButtonSetting)iSetting;
                            var toggle = MakeToggleButton(setting);
                            toggle.transform.SetParent(ui.transform);
                            //var toggleButton = _uiToggleButton.Instantiate<RectTransform>($"{mod.ModName}_{setting.SettingsName}");
                            //toggleButton.transform.SetParent(ui.transform);

                            

                            break;
                    }
                }
            }
        }

        private RectTransform MakeToggleButton(ToggleButtonSetting setting)
        {
            var toggleButton = _uiToggleButton.Instantiate<RectTransform>($"{mod.ModName}_{setting.SettingsName}");
            var toggle = toggleButton.GetComponentInChildren<UnityEngine.UI.Toggle>();
            toggle.OnClick(setting.ValueChanged)
        }
    }
}
