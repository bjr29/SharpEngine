using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about the mouse's scroll wheel movement.
    /// </summary>
    public class MouseWheelEventArgs : EventArgs {
        public int Scrolled { get; set; }

        internal MouseWheelEventArgs(int scrolled) {
            Scrolled = scrolled;
        }
    }
}