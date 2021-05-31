using System;

namespace SharpEngine {
    /// <summary>
    /// Stores data about a key press/ release.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs {
        public string Key { get; private set; }

        internal KeyPressedEventArgs(string key) {
            Key = key;
        }
    }
}
