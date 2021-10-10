namespace SharpEngine {
    /// <summary>
    /// An object to be rendered by DrawRenderables.
    /// </summary>
    public interface IRenderable {
        /// <summary>
        /// The position of the object, usually the top left point.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the object.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Should be rendered by DrawRenderables.
        /// </summary>
        public bool Show { get; set; }
        /// <summary>
        /// The order to be rendered in, only applied by DrawRenderables.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// The render method.
        /// </summary>
        public void Draw();
    }
}