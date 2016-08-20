namespace Assets.Scripts.Misc
{
    public enum BuilderMode { DesignMode, Running }
    public enum InputMode { DragAndDrop, Buttons }

    public static class Globals
    {        
#if UNITY_ANDROID
        public static InputMode InputMode = InputMode.DragAndDrop;
#endif

#if UNITY_IOS
        public static InputMode InputMode = InputMode.DragAndDrop;
#endif

#if UNITY_STANDALONE_WIN
        public static InputMode InputMode = InputMode.Buttons;
#endif   

        public static BuilderMode BuildMode;
    }
}
