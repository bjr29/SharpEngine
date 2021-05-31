using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about 
    /// </summary>
    public class MouseMotionEventArgs : EventArgs {
        public IntVector2 MousePosition { get; private set; }
        public IntVector2 GlobalMousePosition { get; private set; }
        public IntVector2 Motion { get; private set; }

        internal MouseMotionEventArgs(IntVector2 motion, IntVector2 mousePosition, IntVector2 globalMousePosition) {
            Motion = motion;
            MousePosition = mousePosition;
            GlobalMousePosition = globalMousePosition;
        }
    }
}
