using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Used to hold a 4D vector.
    /// </summary>
    public struct Rect {
        /// <summary>
        /// The top left position of the rectangle.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// The size of the rectangle.
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Creates a rectangle from 2 vector2s.
        /// </summary>
        /// <param name="position">The top left of the rectangle.</param>
        /// <param name="size">The size of the rectangle.</param>
        public Rect(Vector2 position, Vector2 size) {
            Position = position;
            Size = size;
        }

        /// <summary>
        /// Creates a rectangle from 4 floats.
        /// </summary>
        /// <param name="x">The x position of the top left of the rectangle.</param>
        /// <param name="y">The y position of the top left of the rectangle.</param>
        /// <param name="w">The width of the rectangle.</param>
        /// <param name="h">The height of the rectangle.</param>
        public Rect(float x, float y, float w, float h) {
            Position = new(x, y);
            Size = new(w, h);
        }

        internal Rect(SDL_Rect rect) {
            Position = new(rect.x, rect.y);
            Size = new(rect.w, rect.h);
        }

        internal Rect(SDL_FRect rect) {
            Position = new(rect.x, rect.y);
            Size = new(rect.w, rect.h);
        }

        /// <summary>
        /// Checks if a point overlaps the rectangle.
        /// </summary>
        /// <param name="point">The point to check if it overlaps.</param>
        /// <returns>Whether there's an overlap.</returns>
        public bool HasOverlapped(Vector2 point) =>
            Position.X <= point.X &&
            Position.X + Size.X >= point.X &&
            Position.Y <= point.Y &&
            Position.Y + Size.Y >= point.Y;

        /// <summary>
        /// Checks if a point overlaps an edge on the rectangle.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>Whether there's an overlap.</returns>
        public bool IsOnEdge(Vector2 point) =>
            Position.X == point.X &&
            Position.X + Size.X == point.X &&
            Position.Y == point.Y &&
            Position.Y + Size.Y == point.Y;

        /// <summary>
        /// Checks if a point overlaps the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to check.</param>
        /// <param name="point">The point to check.</param>
        /// <returns>Whether there's an overlap.</returns>
        public static bool HasOverlapped(Rect rect, Vector2 point) =>
            rect.Position.X <= point.X &&
            rect.Position.X + rect.Size.X >= point.X &&
            rect.Position.Y <= point.Y &&
            rect.Position.Y + rect.Size.Y >= point.Y;

        /// <summary>
        /// Checks if a point overlaps an edge on the rectangle.
        /// </summary>
        /// <param name="rect">The rectangle to check.</param>
        /// <param name="point">The point to check.</param>
        /// <returns>Whether there's an overlap.</returns>
        public static bool IsOnEdge(Rect rect, Vector2 point) =>
            rect.Position.X == point.X &&
            rect.Position.X + rect.Size.X == point.X &&
            rect.Position.Y == point.Y &&
            rect.Position.Y + rect.Size.Y == point.Y;

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
    }
}
