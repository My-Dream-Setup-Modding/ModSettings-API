using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UI.CustomElements.Tabs.SettingsTabs;
using UnityEngine.UI;
using UnityEngine;

namespace ModSettingsApi.Manager
{
    //Partial side of the Settingsmanager, to handle all Unity UI generation.
    internal partial class SettingsManager
    {
        private void BuildModdedPanel()
        {
            _panel = _ui.settingsPanel.gameObject.Instantiate<RectTransform>("ModSettingsPanel");
            var test = _panel.GetComponentsInChildren<HorizontalLayoutGroup>();
            var t2 = _panel.GetComponentInChildren<SettingsTab>();
            foreach (var t in test)
                LogManager.Message($"Found in Panel: {t.name}");
        }

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
