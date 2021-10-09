using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Used to show a point in a 2D space using int values.
    /// </summary>
    public struct IntVector2 : IEquatable<IntVector2> {
        #region Constants/ readonlys
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly IntVector2 Up = new(y: 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly IntVector2 Left = new(x: -1);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly IntVector2 Down = new(y: -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly IntVector2 Right = new(x: 1);
        /// <summary>
        /// 0, 0
        /// </summary>
        public static readonly IntVector2 Zero = new();
        #endregion

        #region Properties
        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        public int Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public IntVector2(int x = 0, int y = 0) {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
        public static IntVector2 operator +(IntVector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static IntVector2 operator -(IntVector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static IntVector2 operator /(IntVector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static IntVector2 operator *(IntVector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static IntVector2 operator +(IntVector2 a, int b) => new(a.X + b, a.Y + b);
        public static IntVector2 operator -(IntVector2 a, int b) => new(a.X - b, a.Y - b);
        public static IntVector2 operator /(IntVector2 a, int b) => new(a.X / b, a.Y / b);
        public static IntVector2 operator *(IntVector2 a, int b) => new(a.X * b, a.Y * b);

        public static IntVector2 operator +(int a, IntVector2 b) => new(a + b.X, a + b.Y);
        public static IntVector2 operator -(int a, IntVector2 b) => new(a - b.X, a - b.Y);
        public static IntVector2 operator /(int a, IntVector2 b) => new(a / b.X, a / b.Y);
        public static IntVector2 operator *(int a, IntVector2 b) => new(a * b.X, a * b.Y);

        public static IntVector2 operator +(IntVector2 a, float b) => new(a.X + (int)b, a.Y + (int)b);
        public static IntVector2 operator -(IntVector2 a, float b) => new(a.X - (int)b, a.Y - (int)b);
        public static IntVector2 operator /(IntVector2 a, float b) => new(a.X / (int)b, a.Y / (int)b);
        public static IntVector2 operator *(IntVector2 a, float b) => new(a.X * (int)b, a.Y * (int)b);

        public static IntVector2 operator +(float a, IntVector2 b) => new((int)a + b.X, (int)a + b.Y);
        public static IntVector2 operator -(float a, IntVector2 b) => new((int)a - b.X, (int)a - b.Y);
        public static IntVector2 operator /(float a, IntVector2 b) => new((int)a / b.X, (int)a / b.Y);
        public static IntVector2 operator *(float a, IntVector2 b) => new((int)a * b.X, (int)a * b.Y);

        public static IntVector2 operator +(IntVector2 a, double b) => new(a.X + (int)b, a.Y + (int)b);
        public static IntVector2 operator -(IntVector2 a, double b) => new(a.X - (int)b, a.Y - (int)b);
        public static IntVector2 operator /(IntVector2 a, double b) => new(a.X / (int)b, a.Y / (int)b);
        public static IntVector2 operator *(IntVector2 a, double b) => new(a.X * (int)b, a.Y * (int)b);

        public static IntVector2 operator +(double a, IntVector2 b) => new((int)a + b.X, (int)a + b.Y);
        public static IntVector2 operator -(double a, IntVector2 b) => new((int)a - b.X, (int)a - b.Y);
        public static IntVector2 operator /(double a, IntVector2 b) => new((int)a / b.X, (int)a / b.Y);
        public static IntVector2 operator *(double a, IntVector2 b) => new((int)a * b.X, (int)a * b.Y);

        public static bool operator ==(IntVector2 a, IntVector2 b) => Equals(a, b);
        public static bool operator !=(IntVector2 a, IntVector2 b) => !Equals(a, b);
        #endregion

        #region Methods
        public static implicit operator Vector2(IntVector2 vector2) => new(vector2.X, vector2.Y);

        public override string ToString() => $"{X},{Y}";

        internal static SDL_Rect ToSDL_Rect(IntVector2 point1, IntVector2 point2) =>
            new() { x = point1.X, y = point1.Y, w = point2.X, h = point2.Y};
        #endregion

        #region Inherited Methods
        public bool Equals(IntVector2 other) {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }
        #endregion
    }
}
