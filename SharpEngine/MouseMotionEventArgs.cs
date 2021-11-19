using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about the mouse's movement.
    /// </summary>
    public class MouseMotionEventArgs : EventArgs {
        /// <summary>
        /// The position of the cursor in the window.
        /// </summary>
        public IntVector2 MousePosition { get; private set; }
        /// <summary>
        /// The position of the cursor in the screen.
        /// </summary>
        public IntVector2 GlobalMousePosition { get; private set; }
        /// <summary>
        /// The velocity of the mouse.
        /// </summary>
        public IntVector2 Motion { get; private set; }

        internal MouseMotionEventArgs(IntVector2 motion, IntVector2 mousePosition, IntVector2 globalMousePosition) {
            Motion = motion;
            MousePosition = mousePosition;
            GlobalMousePosition = globalMousePosition;
        }
    }
}
