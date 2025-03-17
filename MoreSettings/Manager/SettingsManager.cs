using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UI.CustomElements.Tabs.SettingsTabs;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Manager
{
    internal partial class SettingsManager
    {
        private static SettingsManager _instance;
        private static bool _initialized;

        private readonly MainMenuUI _ui;
        private bool _modSettingsOpened;
        //Stuff for the Main Menu UI.
        private Button _vanillaSettingsButton;
        private TextMeshProUGUI _vanillaSettingsText;
        private Button _moddedButton;
        private TextMeshProUGUI _moddedText;

        //Inside of the ModSettings UI.
        private RectTransform _panel;
        private HorizontalLayoutGroup _tabs;
        private SettingsTab _view;

        public static SettingsManager Instance
        {
            private set => _instance = value;
            get
            {
                if (_instance is null)
                    throw new Exception("Please call Init(MainMenuUI Object)");

                return _instance;
            }
        }

        protected SettingsManager(MainMenuUI ui)
        {
            _ui = ui;
        }

        public static void Init(MainMenuUI ui)
        {
            if (_initialized)
                throw new Exception("SettingsManager is already initialized!");
            Instance = new SettingsManager(ui);
            Instance.Init();
        }

        private void Init()
        {
            if (_initialized)
                throw new Exception("SettingsManager is already initialized!");

            //Getting all of the ui objects needed.
            _vanillaSettingsButton = _ui.openSettingsButton;
            _vanillaSettingsText = _ui.gameObject.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(x => x.gameObject.name == "SettingsText");

            BuildModSettingsButton();
            BuildModdedPanel();

            _moddedButton.onClick.AddListener(ToggleModSettings);
        }

        public void Testing()
        {
            var uwu = _panel.GetComponentsInChildren<Transform>();
            foreach (var t in uwu)
                LogManager.Debug($"Found in Panel: {t.name}");
            var test = _panel.GetComponent<HorizontalLayoutGroup>();
        }

        public void OpenModSettings()
        {
            _ui.CloseRoomSelection();
            _ui.CloseWorkshop();
            _ui.CloseCredits();
            _ui.HideNews();
            _ui.CloseSettings();
            _modSettingsOpened = true;
            _panel.transform.DOKill();
            _panel.transform.DOScale(1.1f, 0.25f).
                OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>(
                () => _panel.transform.DOScale(1f, 0.1f));
        }

        public void CloseModSettings()
        {
            _ui.ShowNews();
            _modSettingsOpened = false;
            _panel.transform.DOKill();
            _panel.transform.DOScale(0.0f, 0.25f);
        }

        private void ToggleModSettings()
        {
            if(_modSettingsOpened)
                CloseModSettings();
            else
                OpenModSettings();
        }
    }
}
