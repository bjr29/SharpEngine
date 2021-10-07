namespace SharpEngine {
    /// <summary>
    /// Stores data about a key pressed/ released.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs {
        /// <summary>
        /// The key pressed/ released that invoked the event.
        /// </summary>
        public string Key { get; private set; }

        internal KeyPressedEventArgs(string key) {
            Key = key;
        }
    }
}
