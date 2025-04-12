using ModSettingsApi.Models.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.Events;
using System.Linq;
using UnityEngine;
using static TMPro.TMP_Dropdown;

namespace ModSettingsApi.Models.Variants
{
    public class ComboBoxVariant : IVariant
    {
        public string SettingsText { get; }
        public SettingsVariant Variant => SettingsVariant.ComboBox;
        public List<ComboBoxOptionData> Settings { get; }
        public ComboBoxOptionData CurrentValue { get; private set; }
        public virtual UnityAction<ComboBoxOptionData> ValueChanged { get; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public ComboBoxVariant(string settingsName, UnityAction<ComboBoxOptionData> valueChanged, List<ComboBoxOptionData> settings) : this(settingsName, valueChanged, settings, settings.FirstOrDefault())
        { }

        public ComboBoxVariant(string settingsName, UnityAction<ComboBoxOptionData> valueChanged, List<ComboBoxOptionData> settings, ComboBoxOptionData defaultValue)
        {
            Settings = settings;
            SettingsText = settingsName;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }

        internal void ValueHasChanged(int index)
        {
            if (CurrentValue != Settings[index])
                return;

            CurrentValue = Settings[index];
            ValueChanged(CurrentValue);
        }
    }

    public class ComboBoxOptionData
    {
        public string Text { get; set; }

        public Sprite Image { get; set; }

        public ComboBoxOptionData(string text = null, Sprite image = null)
        {
            Text = text;
            this.Image = image;
        }

        public static implicit operator OptionData(ComboBoxOptionData data)
        {
            return new OptionData(data.Text, data.Image);
        }
    }
}
