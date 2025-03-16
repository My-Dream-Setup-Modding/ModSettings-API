using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Manager
{
    internal class SettingsManager
    {
        private static SettingsManager _instance;
        private static bool _initialized;

        private readonly MainMenuUI _ui;
        private bool _modSettingsOpened;
        private Button _vanillaSettingsButton;
        private TextMeshProUGUI _vanillaSettingsText;
        private Button _moddedButton;
        private TextMeshProUGUI _moddedText;
        private TextMeshProUGUI _moddedTextRow2;

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

            //Setting the Mod Settings text.
            //_moddedTextRow2 = _moddedText.gameObject.Instantiate<TextMeshProUGUI>("ModSettingsText2");
            //Moving Text stuff

            _moddedButton.onClick.AddListener(ToggleModSettings);
        }

        public void OpenModSettings()
        {
            _ui.CloseRoomSelection();
            _ui.CloseWorkshop();
            _ui.CloseCredits();
            _ui.HideNews();
            _ui._settingsOpened = true;
            _ui.settingsPanel.transform.DOKill();
            _ui.settingsPanel.transform.DOScale(1.1f, 0.25f).
                OnComplete<TweenerCore<Vector3, Vector3, VectorOptions>>((TweenCallback)
                (() => _ui.settingsPanel.transform.DOScale(1f, 0.1f)));
        }

        public void CloseModSettings()
        {
            _ui.ShowNews();
            _modSettingsOpened = false;
            _ui.settingsPanel.transform.DOKill();
            _ui.settingsPanel.transform.DOScale(0.0f, 0.25f);
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
