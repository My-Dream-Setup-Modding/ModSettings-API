using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System.Collections.Generic;
using UI.CustomElements.Tabs.SettingsTabs;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using ModSettingsApi.Models;
using TMPro;
using ModSettingsApi.Models.Variants;
using Button = UnityEngine.UI.Button;
using ModSettingsApi.Models.Ui;
using ModSettingsApi.Models.UiWrapper;

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
        private GameObject _tabButton2;
        private TextMeshProUGUI _tabButton2Text;
        private TextMeshProUGUI _tabButtonText;

        /// <summary>
        /// Singleton Pattern mainly used, 
        /// </summary>mich ärg
        public static PanelUiManager Instance { get; set; }

        public Dictionary<TabModel, GameObject> Views { get; set; }
            = new Dictionary<TabModel, GameObject>();

        /// <summary>
        /// The current Tabmodel, that is active.
        /// </summary>
        public TabModel CurrentActive { get; set; }

        /// <summary>
        /// A mod to used ui settings wrapper map, mainly used to help debugging via unityexplorer.
        /// </summary>
        public Dictionary<TabModel, List<object>> DebugComponentList { get; set; } = new Dictionary<TabModel, List<object>>();

        public bool IsOpen { get; set; }

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
                OpenApiView();
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
                (() => _panel.transform.DOScale(1f, 0.1f)));

            IsOpen = true;
        }

        public void ClosePanel()
        {
            if (!IsOpen)
                return;

            _ui.ShowNews();
            //_panel.transform.DOKill();
            _panel.transform.DOScale(0.0f, 0.25f);
            IsOpen = false;
        }

        private void Initialize()
        {
            LogManager.Message("Instatiate Tab elements.");

            var closeButton = _panel.transform.Find("CloseButton").GetComponent<Button>();
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(ClosePanel);

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
                        if (_tabButton != null) continue;
                        _tabButton = child.gameObject;
                        _tabButton.transform.SetParent(_tabs.transform);
                        _tabButtonText = _tabButton.GetComponentInChildren<TextMeshProUGUI>();
                        _tabButtonText.SetText("Modded Tab");
                        var btn = _tabButton.GetComponent<Button>();
                        btn.onClick.RemoveAllListeners();
                        btn.onClick.AddListener(OpenApiView);

                        break;
                    case "Graphics":
                        if (_tabButton2 != null) continue;
                        _tabButton2 = child.gameObject;
                        _tabButton2.transform.SetParent(_tabs.transform);
                        _tabButton2Text = _tabButton2.GetComponentInChildren<TextMeshProUGUI>();
                        var btn2 = _tabButton2.GetComponent<Button>();
                        btn2.onClick.RemoveAllListeners();
                        btn2.onClick = new Button.ButtonClickedEvent();
                        btn2.onClick.AddListener(OpenModView);

                        _tabButton2.SetActive(false);
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

                        var settingApiTab = new TabModel("ModSettingsAPI", new List<IVariant>());
                        Views.Add(settingApiTab, _modView);

                        foreach (var mod in _modsToRender)
                        {
                            var modView = CreateModView(generalView, $"ModView_{mod.ModName}");
                            settingApiTab.Settings.Add(new ButtonVariant(mod.ModName, "Open", x => OpenModView(mod)));
                            Views.Add(mod, modView.gameObject);
                        }

                        //Destroy the old general Settings object.
                        UnityEngine.Object.Destroy(generalView.gameObject);
                        break;

                    case "Graphics":
                        var view = child.GetComponentInChildren<VerticalLayoutGroup>(true);
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

                        UnityEngine.Object.Destroy(view.gameObject);

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
            var existingTextBox = _ui.workshopPanel.Find("FurnitureUpload/WorkshopTitle");
            _uiTextBox = SettingTextBoxWrapper.Create(_uiToggleButton, existingTextBox.gameObject);

            var existingbutton = _ui.workshopPanel.Find("FurnitureUpload/UploadToWorkshop");
            _uiButton = SettingButtonWrapper.Create(_uiToggleButton, existingbutton.gameObject);

            _uiComboBox.ManagedGameObject.SetActive(false);
            _uiToggleButton.ManagedGameObject.SetActive(false);
            _uiSlider.ManagedGameObject.SetActive(false);
            _uiTextBox.ManagedGameObject.SetActive(false);
            _uiButton.ManagedGameObject.SetActive(false);
        }

        private void OpenModView(TabModel openMod)
        {
            LogManager.Message($"Open mod {openMod.ModName}");
            CurrentActive = openMod;
            _tabButton2Text.SetText($"{openMod.ModName}");
            OpenModView();
        }

        private void OpenApiView()
        {
            LogManager.Message($"Open API View.");
            foreach (var view in Views)
                view.Value.SetActive(false);

            _tabButton2Text.color = new Color(0.7f, 0.7f, 0.7f);
            _tabButtonText.color = new Color(1f, 1f, 1f);

            _modView.SetActive(true);
        }

        private void OpenModView()
        {
            foreach (var view in Views)
            {
                if (view.Key == CurrentActive)
                    view.Value.SetActive(true);
                else
                    view.Value.SetActive(false);
            }

            _tabButton2.SetActive(true);
            _tabSplitter.SetActive(true);

            _tabButtonText.color = new Color(0.7f, 0.7f, 0.7f);
            _tabButton2Text.color = new Color(1f, 1f, 1f);
        }
    }
}
