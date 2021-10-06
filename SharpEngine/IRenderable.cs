namespace SharpEngine {
    public interface IRenderable {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public bool Show { get; set; }
        public int ZIndex { get; set; }

        public void Draw();
    }
}