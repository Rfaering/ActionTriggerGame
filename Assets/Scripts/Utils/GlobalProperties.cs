using Assets.Scripts.Canvas.Elements;
using Assets.Scripts.Canvas.Overlays;
using Assets.Scripts.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class GlobalProperties
    {
        public static bool IsOverlayPanelOpen;

        public static bool IsInBuildMode()
        {
            return Globals.BuildMode == BuilderMode.DesignMode;
        }
    }
}
