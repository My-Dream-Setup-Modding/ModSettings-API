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
        private static PanelUiManager _panelManager;
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

            _moddedButton.onClick.AddListener(ToggleModSettings);
        }

        public void Testing()
        {
            var uwu = _panel.GetComponentsInChildren<Transform>();
            foreach (var t in uwu)
                LogManager.Debug($"Found in Panel: {t.name}");
            var test = _panel.GetComponent<HorizontalLayoutGroup>();
        }

        private void ToggleModSettings()
        {
            if (_panelManager is null)
                _panelManager = new PanelUiManager(_ui);

            if(_modSettingsOpened)
                _panelManager.OpenPanel();
            else
                _panelManager.ClosePanel();

            _modSettingsOpened = !_modSettingsOpened;
        }

        /// <summary>
        /// Generate the ModSettings Button in the MainMenu.
        /// </summary>
        private void BuildModSettingsButton()
        {
            _moddedButton = _ui.openSettingsButton.gameObject.Instantiate<Button>("ModSettingsOpenButton");
            _moddedText = _vanillaSettingsText.gameObject.Instantiate<TextMeshProUGUI>("ModSettingsText");

            LogManager.Message($"Changing ModSettings Text Fontsize from {_moddedText.fontSize} to 44");
            _moddedText.fontSize = 44;
            _moddedText.SetText("ModSettings");

            var rectOpen = _vanillaSettingsButton.GetComponent<RectTransform>();
            var rextTextOpen = _vanillaSettingsText.GetComponent<RectTransform>();
            var rectModOpen = _moddedButton.GetComponent<RectTransform>();
            var rectModTextOpen = _moddedText.GetComponent<RectTransform>();

            //This RectTransform is from the rooms button one row above the settings Button.
            //Used to calculate how much space is normally left beween two buttons.
            var roomsRect = _ui.openRoomSelectionButton.GetComponent<RectTransform>();

            //Calculating and moving stuff to the right position.
            var diffBetweenButtons = roomsRect.position.y - roomsRect.sizeDelta.y - rectOpen.position.y;
            var margin = diffBetweenButtons / 2;

            var fullLength = rectOpen.sizeDelta.x;

            var singleLength = fullLength / 2 - margin;
            LogManager.Message($"pixels between buttons: {diffBetweenButtons}, button length is: {fullLength}, singleLength: {singleLength}");

            rectOpen.sizeDelta = new Vector2(singleLength, rectOpen.sizeDelta.y);
            rextTextOpen.sizeDelta = new Vector2(singleLength, rextTextOpen.sizeDelta.y);
            rectModOpen.sizeDelta = new Vector2(singleLength, rectModOpen.sizeDelta.y);
            rectModTextOpen.sizeDelta = new Vector2(singleLength, rectModTextOpen.sizeDelta.y);

            rectOpen.position = new Vector3(rectOpen.position.x - (singleLength / 2 + margin), rectOpen.position.y, rectOpen.position.z);
            rextTextOpen.position = new Vector3(rextTextOpen.position.x - (singleLength / 2 + margin), rextTextOpen.position.y, rextTextOpen.position.z);
            rectModOpen.position = new Vector3(rectModOpen.position.x + (singleLength / 2 + margin), rectModOpen.position.y, rectModOpen.position.z);
            rectModTextOpen.position = new Vector3(rectModTextOpen.position.x + (singleLength / 2 + margin), rectModTextOpen.position.y, rectModTextOpen.position.z);
        }
    }
}
