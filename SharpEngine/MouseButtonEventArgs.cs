using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about a mouse button press/ release.
    /// </summary>
    public class MouseButtonEventArgs : EventArgs {
        public Input.MouseButton MouseButton { get; set; }

        internal MouseButtonEventArgs(Input.MouseButton mouseButton) {
            MouseButton = mouseButton;
        }
    }
}