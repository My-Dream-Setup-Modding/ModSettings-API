﻿using LeTai.Asset.TranslucentImage.Demo;
using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using static TMPro.TMP_Dropdown;

namespace ModSettingsApi.Models.Ui
{
    public class SettingComboBoxWrapper : BaseSetting<ComboBoxVariant, SettingComboBoxWrapper>
    {
        private readonly TextMeshProUGUI _text;
        private readonly TMP_Dropdown _dropDown;

        public SettingComboBoxWrapper(GameObject managedGameObject)
        {
            _managedGameObject = managedGameObject;
            _text = _managedGameObject.transform.Find("Resolution").GetComponent<TextMeshProUGUI>();
            _dropDown = _managedGameObject.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        }

        public override SettingComboBoxWrapper Instatiate(Transform parent, ComboBoxVariant settingModel)
        {
            var gmObj = GameObject.Instantiate(ManagedGameObject, parent);
            gmObj.SetActive(true);
            gmObj.name = $"{((IVariant)settingModel).ParentMod.ModName}_Combo_{settingModel.SettingsText}";
            var combo = new SettingComboBoxWrapper(gmObj);
            //Switching to the copied gameobject context.
            return combo.Instantiate(settingModel);
        }

        private SettingComboBoxWrapper Instantiate(ComboBoxVariant settingModel)
        {
            _dropDown.options.Clear();
            for (int i = 0; i < settingModel.Settings.Count; i++)
            {
                var setting = settingModel.Settings[i];
                var dropDownData = new OptionData(setting.Text, setting.Image);
                _dropDown.options.Add(dropDownData);

                if (setting == settingModel.CurrentValue)
                    _dropDown.value = i;
            }

            _dropDown.onValueChanged?.RemoveAllListeners();
            _dropDown.onValueChanged = new TMP_Dropdown.DropdownEvent();
            _dropDown.onValueChanged.AddListener(settingModel.ValueHasChanged);

            _text.SetText(settingModel.SettingsText);

            return this;
        }
    }
}
