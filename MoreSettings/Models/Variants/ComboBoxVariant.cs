using ModSettingsApi.Models.Enums;
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
        public List<IComboBoxData> Settings { get; }
        public IComboBoxData CurrentValue { get; private set; }
        public virtual UnityAction<IComboBoxData> ValueChanged { get; }

        /// <inheritdoc/>
        [IgnoreDataMember]
        TabModel IVariant.ParentMod { get; set; }

        public ComboBoxVariant(string settingsName, UnityAction<IComboBoxData> valueChanged, List<IComboBoxData> settings) : this(settingsName, valueChanged, settings, settings.FirstOrDefault())
        { }

        public ComboBoxVariant(string settingsName, UnityAction<IComboBoxData> valueChanged, List<IComboBoxData> settings, IComboBoxData defaultValue)
        {
            Settings = settings;
            SettingsText = settingsName;
            ValueChanged = valueChanged;
            CurrentValue = defaultValue;
        }

        internal void ValueHasChanged(int index)
        {
            if (CurrentValue == Settings[index])
                return;

            CurrentValue = Settings[index];
            ValueChanged(CurrentValue);
        }
    }

    public class ComboBoxOptionData : IComboBoxData
    {
        public string Text { get; set; }
        public Sprite Image { get; set; }

        public ComboBoxOptionData(string text = null, Sprite image = null)
        {
            Text = text;
            Image = image;
        }
    }

    public interface IComboBoxData
    {
        public string Text { get; set; }
        public Sprite Image { get; set; }
    }
}
