using static SDL2.SDL;

namespace SharpEngine {
    public struct Rect {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rect(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

        internal SDL_Rect ToSDL_Rect() =>
            new() {
                x = (int)Position.X,
                y = (int)Position.Y,
                w = (int)Size.X,
                h = (int)Size.Y
            };

        internal SDL_FRect ToSDL_FRect() =>
            new() {
                x = Position.X,
                y = Position.Y,
                w = Size.X,
                h = Size.Y
            };

        public static bool HasOverlapped(Rect rect, Vector2 point) =>
            rect.Position.X <= point.X &&
            rect.Position.X + rect.Size.X >= point.X &&
            rect.Position.Y <= point.Y &&
            rect.Position.Y + rect.Size.Y >= point.Y;

        public static bool IsOnEdge(Rect rect, Vector2 point) =>
            rect.Position.X == point.X &&
            rect.Position.X + rect.Size.X == point.X &&
            rect.Position.Y == point.Y &&
            rect.Position.Y + rect.Size.Y == point.Y;
    }
}
