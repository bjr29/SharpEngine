using static SDL2.SDL;

namespace SharpEngine {
    /// <summary>
    /// Used to show a point in a 2D space using float values.
    /// </summary>
    public struct Vector2 : IEquatable<Vector2>, IEquatable<IntVector2> {
        #region Constants/ readonlys
        /// <summary>
        /// 0, 1
        /// </summary>
        public static readonly Vector2 Up = new(y: 1);
        /// <summary>
        /// -1, 0
        /// </summary>
        public static readonly Vector2 Left = new(x: -1);
        /// <summary>
        /// 0, -1
        /// </summary>
        public static readonly Vector2 Down = new(y: -1);
        /// <summary>
        /// 1, 0
        /// </summary>
        public static readonly Vector2 Right = new(x: 1);
        /// <summary>
        /// 0, 0
        /// </summary>
        public static readonly Vector2 Zero = new();
        #endregion
        
        #region Properties
        /// <summary>
        /// The x position of the vector.
        /// </summary>
        public float X { get; set; }
        /// <summary>
        /// The y position of the vector.
        /// </summary>
        public float Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a vector.
        /// </summary>
        /// <param name="x">The x position of the vector.</param>
        /// <param name="y">The y position of the vector.</param>
        public Vector2(float x = 0, float y = 0) {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
        public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(Vector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(Vector2 a, IntVector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(Vector2 a, IntVector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(Vector2 a, IntVector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(Vector2 a, IntVector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(IntVector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
        public static Vector2 operator -(IntVector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator /(IntVector2 a, Vector2 b) => new(a.X / b.X, a.Y / b.Y);
        public static Vector2 operator *(IntVector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);

        public static Vector2 operator +(Vector2 a, int b) => new(a.X + b, a.Y + b);
        public static Vector2 operator -(Vector2 a, int b) => new(a.X - b, a.Y - b);
        public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
        public static Vector2 operator *(Vector2 a, int b) => new(a.X * b, a.Y * b);

        public static Vector2 operator +(int a, Vector2 b) => new(a + b.X, a + b.Y);
        public static Vector2 operator -(int a, Vector2 b) => new(a - b.X, a - b.Y);
        public static Vector2 operator /(int a, Vector2 b) => new(a / b.X, a / b.Y);
        public static Vector2 operator *(int a, Vector2 b) => new(a * b.X, a * b.Y);

        public static Vector2 operator +(Vector2 a, float b) => new(a.X + b, a.Y + b);
        public static Vector2 operator -(Vector2 a, float b) => new(a.X - b, a.Y - b);
        public static Vector2 operator /(Vector2 a, float b) => new(a.X / b, a.Y / b);
        public static Vector2 operator *(Vector2 a, float b) => new(a.X * b, a.Y * b);

        public static Vector2 operator +(float a, Vector2 b) => new(a + b.X, a + b.Y);
        public static Vector2 operator -(float a, Vector2 b) => new(a - b.X, a - b.Y);
        public static Vector2 operator /(float a, Vector2 b) => new(a / b.X, a / b.Y);
        public static Vector2 operator *(float a, Vector2 b) => new(a * b.X, a * b.Y);

        public static Vector2 operator +(Vector2 a, double b) => new(a.X + (float)b, a.Y + (float)b);
        public static Vector2 operator -(Vector2 a, double b) => new(a.X - (float)b, a.Y - (float)b);
        public static Vector2 operator /(Vector2 a, double b) => new(a.X / (float)b, a.Y / (float)b);
        public static Vector2 operator *(Vector2 a, double b) => new(a.X * (float)b, a.Y * (float)b);

        public static Vector2 operator +(double a, Vector2 b) => new((float)a + b.X, (float)a + b.Y);
        public static Vector2 operator -(double a, Vector2 b) => new((float)a - b.X, (float)a - b.Y);
        public static Vector2 operator /(double a, Vector2 b) => new((float)a / b.X, (float)a / b.Y);
        public static Vector2 operator *(double a, Vector2 b) => new((float)a * b.X, (float)a * b.Y);

        public static bool operator ==(Vector2 a, Vector2 b) => Equals(a, b);
        public static bool operator !=(Vector2 a, Vector2 b) => !Equals(a, b);
        #endregion

        #region Methods
        public static implicit operator IntVector2(Vector2 vector2) => new((int)vector2.X, (int)vector2.Y);

        public override string ToString() => $"{X},{Y}";

        internal static SDL_FRect ToSDL_FRect(Vector2 position, Vector2 size) =>
            new() { x = (int)position.X, y = (int)position.Y, w = (int)size.X, h = (int)size.Y };
        #endregion

        #region Inherited Methods
        public bool Equals(Vector2 other) {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(IntVector2 other) {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj) {
            return obj is Vector2 && Equals((Vector2)obj);
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y);
        }
        #endregion
    }
}
