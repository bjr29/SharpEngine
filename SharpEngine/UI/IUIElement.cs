namespace SharpEngine.UI {
    /// <summary>
    /// An object that acts as UI.
    /// </summary>
    public interface IUIElement : IRenderable {
        /// <summary>
        /// The background colour of the UI element.
        /// </summary>
        public Colour BackgroundColour { get; set; }
        /// <summary>
        /// The foreground colour of the UI element.
        /// </summary>
        public Colour ForegroundColour { get; set; }
    }
}
