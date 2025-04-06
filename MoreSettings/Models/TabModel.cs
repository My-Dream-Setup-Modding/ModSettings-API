using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace ModSettingsApi.Models
{
    public class TabModel
    {
        private ObservableCollection<IVariant> _settings;

        public string ModName { get; set; }
        /// <summary>
        /// Using an ObservableCollection to support adding settings after initialization. 
        /// </summary>
        public ObservableCollection<IVariant> Settings
        {
            get => _settings;
            set
            {
                if (_settings != null)
                    _settings.CollectionChanged -= SettingsListChanged;

                _settings = value;
                if (value != null)
                {
                    _settings.CollectionChanged += SettingsListChanged;
                    foreach (var setting in value)
                        setting.ParentMod = this;
                }
            }
        }

        public TabModel(string modName, IEnumerable<IVariant> settings)
        {
            ModName = modName;
            Settings = new ObservableCollection<IVariant>(settings);
        }

        //Used alphabetically sorting Tabs.
        public override string ToString()
        {
            return ModName;
        }

        private void SettingsListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach(var item in e.NewItems)
            {
                if (item is IVariant setting)
                {
                    setting.ParentMod = this;
                }
            }
        }
    }
}
