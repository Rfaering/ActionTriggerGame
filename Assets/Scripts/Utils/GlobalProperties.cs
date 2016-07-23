using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Canvas.Overlays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class GlobalProperties
    {
        public static bool IsOverlayPanelOpen()
        {
            return GlobalGameObjects.OverlayManager.Get().GetComponent<OverlayManager>().IsOverlayOpen;
        }

        public static bool IsInBuildMode()
        {
            return GlobalGameObjects.World.Get().GetComponent<BuildMode>().RuntimeMode == Misc.BuilderMode.DesignMode;
        }
    }
}
