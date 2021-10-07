﻿namespace SharpEngine.UI {
    public interface IUIElement : IRenderable {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Colour BackgroundColour { get; set; }
        public Colour ForegroundColour { get; set; }

        public bool Show { get; set; }
        public int ZIndex { get; set; }

        public void Draw();
    }
}