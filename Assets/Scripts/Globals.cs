using Assets.Scripts.Misc;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Globals
    {
        public static Runner Runner;
        public static DesignPanels DesignPanel;

        public static bool IsInBuildMode()
        {
            SetGlobals();
            return Runner.RuntimeMode == BuilderMode.DesignMode;
        }

        public static bool IsDesignPanelOpen()
        {
            SetGlobals();
            return DesignPanel.IsPanelOpen;
        }

        private static void SetGlobals()
        {
            if (DesignPanel == null)
            {
                DesignPanel = GameObject.Find("Canvas/DesignPanel").GetComponent<DesignPanels>();
            }
            if (Runner == null)
            {
                Runner = GameObject.Find("World").GetComponent<Runner>();
            }
        }
    }
}
