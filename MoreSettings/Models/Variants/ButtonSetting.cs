namespace MoreSettings.Models.Variants
{
    public abstract class ButtonSetting : SettingsModel<string>
    {
        public string Value { get; set; }
        public SettingsVariant Variant => SettingsVariant.Button;

        protected ButtonSetting()
        {
            
        }

        public abstract void Click();
    }
}
