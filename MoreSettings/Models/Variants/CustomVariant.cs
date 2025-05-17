using ModSettingsApi.Models.Enums;
using UnityEngine;

namespace ModSettingsApi.Models.Variants
{
    public class CustomVariant : IVariant
    {
        public SettingsVariant Variant => SettingsVariant.Custom;
        public GameObject OverlayComponent { get; set; }
        TabModel IVariant.ParentMod { get; set; }

        /// <summary>
        /// Used to create a new instance of <see cref="OverlayComponent"/> and set its parent, to the stackpanel containing all settings.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public virtual GameObject Instantiate(Transform parent)
        {
            var gmObj = GameObject.Instantiate(OverlayComponent, parent);
            return gmObj;
        }
    }
}
