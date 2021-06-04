using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about a mouse button press/ release.
    /// </summary>
    public class MouseButtonEventArgs : EventArgs {
        /// <summary>
        /// The mouse button pressed/ released that invoked the event.
        /// </summary>
        public Input.MouseButton MouseButton { get; set; }

        internal MouseButtonEventArgs(Input.MouseButton mouseButton) {
            MouseButton = mouseButton;
        }
    }
}