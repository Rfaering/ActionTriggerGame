using Assets.Scripts.Stats;
using Assets.Scripts.Utils;

namespace Assets.Scripts
{
    public static class GlobalGameObjects
    {
        public static CachedGameObject World = new CachedGameObject("World");
        public static CachedGameObject OverlayManager = new CachedGameObject("Canvas/Overlays");
        public static CachedGameObject DesignPanel = new CachedGameObject("Canvas/DesignPanel");
        public static CachedGameObject Canvas = new CachedGameObject("Canvas");
    }
}
