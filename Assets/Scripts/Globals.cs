using Assets.Scripts.Misc;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets
{
    public static class Globals
    {
        public static CachedGameObject<Runner> Runner = new CachedGameObject<Runner>("World");
        public static CachedGameObject<DesignPanels> DesignPanel = new CachedGameObject<DesignPanels>("Canvas/DesignPanel");

        public static bool IsInBuildMode()
        {
            return Runner.Get().RuntimeMode == BuilderMode.DesignMode;
        }

        public static bool IsDesignPanelOpen()
        {
            return DesignPanel.Get().IsOverlayOpen;
        }
    }
}
