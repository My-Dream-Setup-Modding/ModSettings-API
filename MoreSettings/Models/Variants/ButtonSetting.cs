using ModSettingsApi.Models.Enums;

namespace ModSettingsApi.Models.Variants
{
    public class ButtonSetting : ISingleSettingModel
    {
        public delegate void ButtonClick();

        public SettingsVariant Variant => SettingsVariant.Button;
        public string SettingsName { get; set; }
        public ButtonClick Click { get; set; }
        
        public ButtonSetting(string settingsName, ButtonClick click)
        {
            SettingsName = settingsName;
            Click = click;
        }
    }
}
