using ModSettingsApi.Models.UiWrapper;
using ModSettingsApi.Models.Variants;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace ModSettingsApi.Manager
{
    public partial class PanelUiManager
    {
        public GameObject CreateModView(VerticalLayoutGroup existingView, string name)
        {
            var parent = existingView.transform.parent;

            var view = GameObject.Instantiate(existingView, parent, true);
            view.name = name;
            view.transform.localScale = Vector3.one;

            return view.gameObject;
        }

        private void BuildView()
        {

        }
    }
}
