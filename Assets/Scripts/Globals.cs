namespace Assets.Scripts.Misc
{
    public enum BuilderMode { DesignMode, Running }
    public enum InputMode { DragAndDrop, Buttons }

    public static class Globals
    {
#if UNITY_ANDROID
        public static InputMode InputMode = InputMode.DragAndDrop;
        public static BuilderMode BuildMode = BuilderMode.Running;
#endif

#if UNITY_IOS
        public static InputMode InputMode = InputMode.DragAndDrop;
        public static BuilderMode BuildMode = BuilderMode.Running;
#endif

#if UNITY_STANDALONE_WIN
        public static InputMode InputMode = InputMode.Buttons;
        public static BuilderMode BuildMode = BuilderMode.DesignMode;
#endif           

        public static int InitialLevel = 1;
        public static int MaxLevel = 48;        
    }
}
