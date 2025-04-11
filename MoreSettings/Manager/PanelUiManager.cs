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
using Sirenix.Utilities;
using Button = UnityEngine.UI.Button;
using ModSettingsApi.Models.Ui;
using ModSettingsApi.Models.UiWrapper;
using System.Linq;

namespace ModSettingsApi.Manager
{
    /// <summary>
    /// Manager to handle the modded ui panel.
    /// </summary>
    public partial class PanelUiManager
    {
        private readonly MainMenuUI _ui;
        private readonly List<TabModel> _modsToRender;
        private bool _initialized;

        private GameObject _modView;

        private SettingComboBoxWrapper _uiComboBox;
        private SettingToggleButtonWrapper _uiToggleButton;
        private SettingSliderWrapper _uiSlider;

        private SettingTextBoxWrapper _uiTextBox;
        private SettingButtonWrapper _uiButton;

        private GameObject _tabSplitter;
        private GameObject _tabButton;

        private RectTransform _panel;
        private HorizontalLayoutGroup _tabs;

        /// <summary>
        /// Singleton Pattern mainly used, 
        /// </summary>mich ärg
        public static PanelUiManager Instance { get; set; }

        public Dictionary<TabModel, GameObject> Views { get; set; } 
            = new Dictionary<TabModel, GameObject>();
        
        /// <summary>
        /// A mod to used ui settings wrapper map, mainly used to help debugging via unityexplorer.
        /// </summary>
        public Dictionary<TabModel, List<object>> DebugComponentList { get; set; } = new Dictionary<TabModel, List<object>>();

        public PanelUiManager(MainMenuUI gameUi, List<TabModel> modsToRender)
        {
            Instance = this;
            _ui = gameUi;
            _modsToRender = modsToRender;
            _panel = _ui.settingsPanel.gameObject.Instantiate<RectTransform>("ModSettingsPanel");
            _tabs = _panel.GetComponentInChildren<HorizontalLayoutGroup>();
        }

        public void OpenPanel()
        {
            if (!_initialized)
            {
                Initialize();
                BuildView();
                _initialized = true;
            }

            _ui.CloseRoomSelection();
            _ui.CloseWorkshop();
            _ui.CloseCredits();
            _ui.HideNews();
            _ui.CloseSettings();
            //_panel.transform.DOKill();
            _panel.transform.DOScale(1.1f, 0.25f).
                OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(
                () => _panel.transform.DOScale(1f, 0.1f).OnComplete(() =>
                        {

                        }));
        }

        public void ClosePanel()
        {
            _ui.ShowNews();
            //_panel.transform.DOKill();
            _panel.transform.DOScale(0.0f, 0.25f);
        }

        private void Initialize()
        {
            LogManager.Message("Instatiate Tab elements.");

            //Removing all TabButtons on the top, but ModSettings.
            foreach (RectTransform child in _tabs.GetComponentInChildren<RectTransform>())
            {
                LogManager.Message($"Found tab children: {child.name}");
                switch (child.name)
                {
                    case "Splitter":
                        if (_tabSplitter is null)
                        {
                            _tabSplitter = child.gameObject;
                            _tabSplitter.name = "Modded_TabSplitter";
                            _tabSplitter.gameObject.SetActive(false);
                            break;
                        }

                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                    case "General":
                        _tabButton = child.gameObject;
                        _tabButton.transform.SetParent(_tabs.transform);
                        _tabButton.GetComponentInChildren<TextMeshProUGUI>().SetText("Modded Tab");
                        var btn = _tabButton.GetComponent<Button>();
                        btn.onClick = new Button.ButtonClickedEvent();
                        btn.onClick.AddListener(()=> OpenMod(Views.First().Key));

                        break;
                    default:
                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                }
            }

            LogManager.Message("Instatiate inner view setting options.");

            foreach (SettingsTab child in _panel.GetComponentsInChildren<SettingsTab>())
            {
                LogManager.Message($"Found View child: {child.name}");
                switch (child.name)
                {
                    case "General":
                        child.name = "ModSettingsController";

                        var generalView = child.GetComponentInChildren<VerticalLayoutGroup>(true);
                        LogManager.Message($"General found layoutGroup: {(generalView != null ? "Yes" : "No")}");

                        var tempObj = new GameObject("TempObject");
                        //The "General" view does not have any interesting ui setting.
                        foreach (var setting in generalView.GetComponentsInChildren<RectTransform>())
                        {
                            if (setting.name == "TabView") continue;

                            LogManager.Message($"Remove from GeneralView: {setting.name}");
                            setting.gameObject.SetActive(false);
                            setting.transform.SetParent(tempObj.transform);
                        }

                        LogManager.Message($"Creating ModdedView.");
                        //Creating temporary object, since i can't figure out, how to not get an instatiated object destroyed immediately otherwise ...
                        _modView = CreateModView(generalView, "ModdedView");
                        _modView.gameObject.SetActive(true);
                        _modView.GetComponent<VerticalLayoutGroup>().enabled = true;

                        var settingApiTab = new TabModel("ModSettingsAPI", new List<IVariant>());
                        Views.Add(settingApiTab, _modView);

                        foreach (var mod in _modsToRender)
                        {
                            LogManager.Warn($"Adding settings view for {mod.ModName}");

                            var modView = CreateModView(generalView, $"ModView_{mod.ModName}");
                            settingApiTab.Settings.Add(new ButtonVariant(mod.ModName, OpenMod));
                            Views.Add(mod, modView.gameObject);
                        }

                        //Destroy the old general Settings object.
                        UnityEngine.Object.Destroy(generalView.gameObject);

                        break;

                    case "Graphics":
                        var view = child.GetComponentInChildren<VerticalLayoutGroup>(true);
                        LogManager.Message($"Graphics found layoutGroup: {(view != null ? "Yes" : "No")}");
                        foreach (var innerChild in view.GetComponentsInChildren<RectTransform>())
                        {
                            LogManager.Message($"Found in graphic child: {innerChild.name}");
                            if (innerChild.name == "Resolution")
                            {
                                //Idk why it finds two childs with "Resolution".
                                if (_uiComboBox != null)
                                    continue;

                                //Extract ComboBox
                                var combo = GameObject.Instantiate(innerChild.gameObject, _modView.transform);
                                combo.name = "Modded_ComboBox";
                                _uiComboBox = new SettingComboBoxWrapper(combo);
                            }
                            else if (innerChild.name == "FullScreen")
                            {
                                if (_uiToggleButton != null)
                                    continue;

                                //ToggleButton
                                var toggle = GameObject.Instantiate(innerChild.gameObject, _modView.transform);
                                toggle.name = "Modded_Toggle";
                                _uiToggleButton = new SettingToggleButtonWrapper(toggle);
                            }
                        }

                        UnityEngine.Object.Destroy(view);

                        break;
                    case "Audio":
                        //Extracting UI Slider

                        var audioView = child.GetComponentInChildren<VerticalLayoutGroup>(true);
                        LogManager.Message($"Graphics found layoutGroup: {(audioView != null ? "Yes" : "No")}");
                        foreach (var innerChild in audioView.GetComponentsInChildren<RectTransform>())
                        {
                            if (innerChild.name == "Music")
                            {
                                var slider = GameObject.Instantiate(innerChild.gameObject, _modView.transform);
                                slider.name = "Modded_Slider";
                                _uiSlider = new SettingSliderWrapper(slider);

                                break;
                            }
                        }

                        UnityEngine.Object.Destroy(audioView);
                        break;
                    default:
                        UnityEngine.Object.Destroy(child.gameObject);
                        break;
                }
            }

            //TODO Build a button 
            var existingTextBox = _ui.workshopPanel.Find("FurnitureUpload/FilePath");
            _uiTextBox = new SettingTextBoxWrapper(_uiSlider, existingTextBox.gameObject);

            var existingbutton = _ui.workshopPanel.Find("FurnitureUpload/ModelUpload");
            _uiButton = new SettingButtonWrapper(_uiSlider, existingbutton.gameObject);
        }

        private void OpenMod(ButtonVariant sender)
        {
            LogManager.Message($"Button Click from sender {sender.SettingsText}");
            IVariant variant = sender as IVariant;
            OpenMod(variant.ParentMod);
        }

        private void OpenMod(TabModel modToOpen)
        {
            var modView = Views[modToOpen];
            foreach (var view in Views)
            {
                view.Value.SetActive(view.Key == modToOpen);
            }

            ////TODO TabHeader with Splitters logic.
        }
    }
}
