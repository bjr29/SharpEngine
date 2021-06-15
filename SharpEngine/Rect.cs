namespace SharpEngine {
    public class Rect {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rect(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

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
