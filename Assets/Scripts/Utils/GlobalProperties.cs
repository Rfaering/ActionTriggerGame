public class GlobalProperties
{
    public static bool IsOverlayPanelOpen;

    public static bool IsInBuildMode()
    {
        return Globals.BuildMode == BuilderMode.DesignMode;
    }
}
